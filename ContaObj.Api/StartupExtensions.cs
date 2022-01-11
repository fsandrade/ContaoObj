using Serilog;

namespace ContaObj.Api;

public static class StartupExtensions
{
    public static WebApplicationBuilder UseStartup<T>(this WebApplicationBuilder webApplicationBuilder) where T : IStartup
    {
        var startup = Activator.CreateInstance(typeof(T), webApplicationBuilder.Configuration) as IStartup;
        if (startup == null) throw new ArgumentException("Startup class not configured.");

        webApplicationBuilder.Host.UseSerilog((ctx, cfg) => cfg.ReadFrom.Configuration(ctx.Configuration));

        startup.ConfigureServices(webApplicationBuilder.Services);

        var app = webApplicationBuilder.Build();
        startup.Configure(app);

        app.Run();

        return webApplicationBuilder;
    }
}