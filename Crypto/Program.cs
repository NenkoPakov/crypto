using Crypto.Services;
using Crypto.Services.Interfaces;
using Serilog;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Host.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));

        ConfigureServices(builder.Services);

        var app = builder.Build();
        app.UseCors(builder => builder.AllowAnyOrigin()
                                      .AllowAnyMethod()
                                      .AllowAnyHeader());

        app.UseSerilogRequestLogging();
        app.UseAuthorization();
        app.MapControllers();
        app.Run();
    }
    public static void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
        services.AddHttpClient();

        services.AddTransient<ICryptoCurrencyService, CryptoCurrencyService>();
        services.AddTransient<IPortfolioService, PortfolioService>();
    }
}