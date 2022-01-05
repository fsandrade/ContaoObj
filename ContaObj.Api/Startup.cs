using ContaObj.Infra.Database;
using Microsoft.EntityFrameworkCore;
using FluentValidation.AspNetCore;
using ContaObj.Application.Validators;
using ContaObj.Application.Mappings;
using ContaObj.Infra.Repositories;
using ContaObj.Application.Interfaces;
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

        //TODO fazer arquivos de configuração
        //TODO Configurar services e automappers em classe separada

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