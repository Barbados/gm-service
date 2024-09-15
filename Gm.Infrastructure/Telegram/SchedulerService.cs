using Microsoft.Extensions.Hosting; 

namespace Gm.Infrastructure.Telegram;

public class SchedulerService : BackgroundService
{
    private readonly TimeSpan _targetTime = new(8, 0, 0); // Set the target time for sending the message (e.g., 8:00 AM)
    
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
                    : _targetTime.Add(new TimeSpan(24, 0, 0)) - now.TimeOfDay;
            
                await Task.Delay(timeUntilTarget, token);

                if (!token.IsCancellationRequested)
                {
                    //await botClient.SendTextMessageAsync(chatId, "Good morning!");
                    await Task.Delay(TimeSpan.FromDays(1), token); // Wait for 1 day
                }
                //await receiverService.ReceiveAsync(token);
            }
            catch (Exception e)
            {
                await Task.Delay(TimeSpan.FromSeconds(5), token);
            }
        }
    }
}