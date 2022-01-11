namespace ContaObj.Api;

public interface IStartup
{
    IConfiguration Configuration { get; }

    void ConfigureServices(IServiceCollection services);

    void Configure(WebApplication app);
}