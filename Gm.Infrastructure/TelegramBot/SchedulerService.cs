using Gm.Infrastructure.TelegramBot.Abstract;
using Microsoft.Extensions.Hosting;

namespace Gm.Infrastructure.TelegramBot;

public class SchedulerService(ISenderService senderService) : BackgroundService
{
    private TimeSpan _targetTime = new(23, 37, 0); // Set the target time for sending the message (e.g., 8:00 AM)
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await DoWorkAsync(stoppingToken);
        Console.WriteLine("Press ENTER to exit the application.");
        Console.ReadLine();
        Environment.Exit(0);
    }

    private async Task DoWorkAsync(CancellationToken token)
    {
        while (!token.IsCancellationRequested)
        {
            try
            {
                var now = DateTime.Now;
                var timeUntilTarget = _targetTime > now.TimeOfDay 
                    ? _targetTime - now.TimeOfDay 
                    : _targetTime.Add(new TimeSpan(0, 1, 0)) - now.TimeOfDay;
            
                await Task.Delay(timeUntilTarget, token);

                if (!token.IsCancellationRequested)
                {
                    await senderService.SendAsync(token);
                    _targetTime = _targetTime.Add(TimeSpan.FromMinutes(1));
                    await Task.Delay(TimeSpan.FromMinutes(1), token); // Wait for 1 day
                }
            }
            catch (Exception e)
            {
                await Task.Delay(TimeSpan.FromSeconds(5), token);
            }
        }
    }
}