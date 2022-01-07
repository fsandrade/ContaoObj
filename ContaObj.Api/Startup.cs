using FluentValidation.AspNetCore;
using ContaObj.Application.Validators;
using ContaObj.Api.Configurations;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;

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

        services.AddSwaggerConfiguration();

        services.AddFluentValidationRulesToSwagger();
    }

    public void Configure(WebApplication app)
    {
        app.UseExceptionHandler("/error");

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseDeveloperExceptionPage();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.UsarConfiguracaoDatabase();
    }
}