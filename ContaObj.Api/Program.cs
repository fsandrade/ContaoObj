using ContaObj.Api;
using Serilog;

Log.Information("Iniciando WebAPI");

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Host.UseSerilog((ctx, cfg) => cfg.ReadFrom.Configuration(ctx.Configuration));

    var startup = new Startup(builder.Configuration);
    startup.ConfigureServices(builder.Services);

    var app = builder.Build();

    startup.Configure(app);

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Erro inexperado");
}
finally
{
    Log.Information("Finalizando WebApi");
    Log.CloseAndFlush();
}

