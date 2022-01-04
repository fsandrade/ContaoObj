using ContaObj.Infra.Database;
using Microsoft.EntityFrameworkCore;

namespace ContaObj.Api;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
        services.AddDbContext<ContaObjContext>(options => options.UseSqlServer(Configuration.GetConnectionString("ContaObjDb")));
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
    }

    public void Configure(WebApplication app, IWebHostEnvironment environment)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();
    }
}