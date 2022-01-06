using ContaObj.Infra.Database;
using Microsoft.EntityFrameworkCore;

namespace ContaObj.Api.Configurations
{
    public static class DatabaseConfig
    {
        public static void AdicionarConfiguracaoDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ContaObjContext>(options => options.UseSqlServer(configuration.GetConnectionString("ContaObjDb")));
        }

        public static void UsarConfiguracaoDatabase(this IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
            using var context = serviceScope.ServiceProvider.GetService<ContaObjContext>();
            context.Database.Migrate();
            context.Database.EnsureCreated();
        }
    }
}
