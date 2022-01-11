using ContaObj.Api;
using Serilog;

Log.Information("Iniciando WebAPI");

try
{
    var builder = WebApplication.CreateBuilder(args)
        .UseStartup<Startup>();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Erro inesperado");
}
finally
{
    Log.Information("Finalizando WebApi");
    Log.CloseAndFlush();
}