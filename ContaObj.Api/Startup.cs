using FluentValidation.AspNetCore;
using ContaObj.Application.Validators;
using ContaObj.Api.Configurations;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using ContaObj.Application.Validators.Transacao;

namespace ContaObj.Api;

public class Startup : IStartup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers()
            .AddFluentValidation(p =>
            {
                p.RegisterValidatorsFromAssemblyContaining<NovoClienteValidator>();
                p.RegisterValidatorsFromAssemblyContaining<NovaTransacaoValidator>();
            })
            .AddNewtonsoftJson(x =>
            {
                x.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                x.SerializerSettings.Converters.Add(new StringEnumConverter());
            })
            .AddJsonOptions(p =>
            {
                p.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });

        services.TryAddEnumerable(ServiceDescriptor.Transient<IApplicationModelProvider, ProduceResponseTypeModelProvider>());
        services.AdicionaAutoMapper();

        services.AdicionaConfiguracaoInjecaoDependencia(Configuration);

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

        //app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.UsarConfiguracaoDatabase();
    }
}