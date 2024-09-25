using Gm.Infrastructure.Scheduler.Jobs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Quartz;

namespace Gm.Infrastructure.Configuration.DependencyInjection;

public static class SchedulerExtensions
{
    public static void ConfigureScheduler(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddQuartz(quartz =>
        {
            var jobKey = GmSenderJob.Key;
            quartz.AddJob<GmSenderJob>(opts => 
                opts.WithIdentity(jobKey)
            );

            var jobName = nameof(GmSenderJob);
            var cronSchedule = configuration.GetSection($"Quartz:{jobName}").Value;
            if (cronSchedule == null)
                throw new Exception($"Schedule is not specified for {jobName}");
            
            quartz.AddTrigger(opts => 
                opts.ForJob(jobKey)
                    .WithIdentity($"{jobName}-trigger")
                    .WithCronSchedule(cronSchedule)
            );
        });
        
        services.AddQuartzHostedService(options =>
        {
            options.StartDelay = TimeSpan.FromSeconds(5);
            options.AwaitApplicationStarted = true;
            options.WaitForJobsToComplete = true;
        });
    }
}