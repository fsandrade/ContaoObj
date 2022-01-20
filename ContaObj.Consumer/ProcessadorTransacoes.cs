using ContaObj.Application.Interfaces;
using ContaObj.Domain.Model;
using ContaObj.Domain.Options;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace ContaObj.Consumer
{
    public class ProcessadorTransacoes : BackgroundService
    {
        private readonly ILogger<ProcessadorTransacoes> logger;
        private readonly ITransacaoRepositorio repository;
        private readonly ConnectionFactory factory;

        public ProcessadorTransacoes(ILogger<ProcessadorTransacoes> logger, IOptionsMonitor<RabbitMqOptions> options, ITransacaoRepositorio repository)
        {
            this.logger = logger;
            this.repository = repository;
            factory = new ConnectionFactory() { HostName = options.CurrentValue.HostName };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();
            channel.QueueDeclare(queue: "transacao",
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using var connection = factory.CreateConnection();
                using var channel = connection.CreateModel();
                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += async (model, e) =>
                {
                    try
                    {
                        var body = e.Body.ToArray();
                        var message = Encoding.UTF8.GetString(body);
                        var transacao = JsonSerializer.Deserialize<Transacao>(message);
                        if (transacao == null) throw new ApplicationException("Transação inválida");
                        transacao = await repository.EfetivaDocAsync(transacao);
                        logger.LogInformation("Transação ID: {Id} Data: {Data} Valor: {Valor}", transacao?.Id, transacao?.Data, transacao?.Valor);

                        channel.BasicAck(e.DeliveryTag, false);
                    }
                    catch (Exception ex)
                    {
                        channel.BasicAck(e.DeliveryTag, false);
                        //channel.BasicNack(e.DeliveryTag, false, true);
                        logger.LogError(ex, "Erro ao processar transação");
                    }
                };

                channel.BasicConsume(queue: "transacao",
                                    autoAck: false,
                                    consumer: consumer);

                await Task.Delay(TimeSpan.FromSeconds(60), stoppingToken);
            }
        }
    }
}