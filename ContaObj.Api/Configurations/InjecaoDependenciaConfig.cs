using ContaObj.Application.Interfaces;
using ContaObj.Application.Managers;
using ContaObj.Infra.Repositories;

namespace ContaObj.Api.Configurations
{
    public static class InjecaoDependenciaConfig
    {
        public static void AdicionaConfiguracaoInjecaoDependencia(this IServiceCollection services)
        {
            services.AddScoped<IClienteManager, ClienteManager>();
            services.AddScoped<IClienteRepository, ClienteRepository>();
            services.AddScoped<IContaManager, ContaManager>();
            services.AddScoped<IContaRepository, ContaRepository>();
            services.AddScoped<IAgenciaRepository, AgenciaRepository>();
        }
    }
}
