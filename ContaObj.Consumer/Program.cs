using ContaObj.Application.Interfaces;
using ContaObj.Consumer;
using ContaObj.Domain.Options;
using ContaObj.Infra.Database;
using ContaObj.Infra.Repositories;
using Microsoft.EntityFrameworkCore;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        var configuration = new ConfigurationBuilder()
                           .AddEnvironmentVariables()
                           .AddCommandLine(args)
                           .AddJsonFile("appsettings.json")
                           .Build();
        services.AddHostedService<ProcessadorTransacoes>();
        services.AddSingleton<ITransacaoRepositorio, TransacaoRepositorio>();
        services.AddDbContext<ContaObjContext>(options => options.UseSqlServer(configuration.GetConnectionString("ContaObjDb")), ServiceLifetime.Singleton);
        services.Configure<RabbitMqOptions>(configuration.GetSection("RabbitMq"));
    })
    .Build();

await host.RunAsync();