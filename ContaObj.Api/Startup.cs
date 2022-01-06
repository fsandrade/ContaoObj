using FluentValidation.AspNetCore;
using ContaObj.Application.Validators;
using ContaObj.Api.Configurations;

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
        services.AddControllers().AddFluentValidation(p =>
        {
            p.RegisterValidatorsFromAssemblyContaining<NovoClienteValidator>();
        });

        services.AdicionaAutoMapper();

        services.AdicionaConfiguracaoInjecaoDependencia();

        services.AdicionarConfiguracaoDatabase(Configuration);

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

        app.UsarConfiguracaoDatabase();
    }
}