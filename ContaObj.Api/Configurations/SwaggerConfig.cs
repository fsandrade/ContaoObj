using Microsoft.OpenApi.Models;
using System.Reflection;

namespace ContaObj.Api.Configurations
{
    public static class SwaggerConfig
    {
        public static void AddSwaggerConfiguration(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1",
                    new OpenApiInfo
                    {
                        Title = "Conta Obj",
                        Version = "v1",
                        Description = "API da Conta Obj",
                    });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
                xmlPath = Path.Combine(AppContext.BaseDirectory, "ContaObj.Domain.xml");
                c.IncludeXmlComments(xmlPath);
                c.EnableAnnotations();
            });
        }

    }
}
