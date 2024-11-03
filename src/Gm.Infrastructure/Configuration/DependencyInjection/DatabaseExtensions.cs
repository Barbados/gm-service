using Ardalis.SharedKernel;
using Gm.Infrastructure.Data;
using Gm.Infrastructure.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Gm.Infrastructure.Configuration.DependencyInjection;

public static class DatabaseExtensions
{
    public static IServiceCollection ConfigureDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        using var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
        var logger = loggerFactory.CreateLogger("Gm");
        
        var connectionString = configuration.GetConnectionString("GmDb");
        logger.LogInformation($"Connection string: {connectionString}");

        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseNpgsql(connectionString)
                .UseSnakeCaseNamingConvention();
        });
        
        services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
        services.AddScoped(typeof(IReadRepository<>), typeof(EfRepository<>));

        return services;
    }

    public static void InitializeDatabase(this IHost app)
    {
        using var serviceScope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
        var databaseFacade = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>().Database;
        
        databaseFacade.Migrate();
    }
}