using ContaObj.Application.Interfaces;
using ContaObj.Application.Managers;
using ContaObj.Application.Services;
using ContaObj.Domain.Options;
using ContaObj.Infra.Repositories;

namespace ContaObj.Api.Configurations;

public static class InjecaoDependenciaConfig
{
    public static void AdicionaConfiguracaoInjecaoDependencia(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IClienteManager, ClienteManager>();
        services.AddScoped<IClienteRepository, ClienteRepository>();
        services.AddScoped<IContaManager, ContaManager>();
        services.AddScoped<IContaRepository, ContaRepository>();
        services.AddScoped<IAgenciaRepository, AgenciaRepository>();
        services.AddScoped<ITransacaoManager, TransacaoManager>();
        services.AddScoped<ITransacaoProducer, TransacaoProducer>();
        services.AddScoped<ITransacaoRepositorio, TransacaoRepositorio>();
        services.Configure<RabbitMqOptions>(configuration.GetSection("RabbitMq"));
    }
}