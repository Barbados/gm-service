using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace Gm.Infrastructure.Configuration.DependencyInjection;

public static class LoggerExtensions
{
    public static IServiceCollection ConfigureLogging(this IServiceCollection services)
    {
        const string outputTemplate = "[{Timestamp:yyyy-MM-dd HH:mm:ss.fff} [{Level}] {Message}{NewLine}{Exception}";
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Verbose()
            .WriteTo.Console(
                outputTemplate: outputTemplate)
            .WriteTo.File(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "LogFiles", $"{DateTime.Now.Year}-{DateTime.Now.Month}-{DateTime.Now.Day}", "Log.txt"),
                rollingInterval: RollingInterval.Infinite,
                outputTemplate: outputTemplate)
            .CreateLogger();

        services.AddSerilog();

        return services;
    }
}