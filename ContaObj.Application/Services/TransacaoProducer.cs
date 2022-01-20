using ContaObj.Application.Interfaces;
using ContaObj.Domain.Model;
using ContaObj.Domain.Options;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace ContaObj.Application.Services;

public class TransacaoProducer : ITransacaoProducer
{
    private readonly ConnectionFactory factory;

    public TransacaoProducer(IOptionsMonitor<RabbitMqOptions> options)
    {
        factory = new ConnectionFactory() { HostName = options.CurrentValue.HostName };
        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();

        channel.QueueDeclare(queue: "transacao",
                             durable: false,
                             exclusive: false,
                             autoDelete: false,
                             arguments: null);
    }

    public void EnviaTransacaoParaFila(Transacao transacao)
    {
        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();

        var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(transacao));

        channel.BasicPublish(exchange: "",
                             routingKey: "transacao",
                             basicProperties: null,
                             body: body);
    }
}