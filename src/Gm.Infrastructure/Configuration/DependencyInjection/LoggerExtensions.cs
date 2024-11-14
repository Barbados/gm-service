using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace Gm.Infrastructure.Configuration.DependencyInjection;

public static class LoggerExtensions
{
    public static IServiceCollection ConfigureLogging(this IServiceCollection services, IConfiguration configuration)
    {
        var logsDirectory = configuration["Log:LogDirectoryPath"] ?? string.Empty;
        const string outputTemplate = "[{Timestamp:yyyy-MM-dd HH:mm:ss.fff} [{Level}] {Message}{NewLine}{Exception}";
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Verbose()
            .WriteTo.Console(
                outputTemplate: outputTemplate)
            .WriteTo.File(Path.Combine(logsDirectory, "logs", $"{DateTime.Now.Year}-{DateTime.Now.Month}-{DateTime.Now.Day}", "Log.txt"),
                rollingInterval: RollingInterval.Infinite,
                outputTemplate: outputTemplate)
            .CreateLogger();
        Log.Logger.Information($"Logs directory: {logsDirectory}");

        services.AddSerilog();

        return services;
    }
}