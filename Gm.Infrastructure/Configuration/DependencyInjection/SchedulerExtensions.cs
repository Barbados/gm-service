using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Quartz;

namespace Gm.Infrastructure.Configuration.DependencyInjection;

public static class SchedulerExtensions
{
    public static IServiceCollection ConfigureScheduler(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddQuartz();
        services.AddQuartzHostedService(options =>
        {
            options.StartDelay = TimeSpan.FromSeconds(5);
            options.AwaitApplicationStarted = true;
            options.WaitForJobsToComplete = true;
        });
        
        return services;
    }
    
    
}