using Gm.Infrastructure.TelegramBot.Abstract;
using Microsoft.Extensions.Hosting;

namespace Gm.Infrastructure.TelegramBot.Services.Core;

public class PollingService(IReceiverService receiverService) : BackgroundService
{
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
                await receiverService.ReceiveAsync(token);
            }
            catch (Exception e)
            {
                await Task.Delay(TimeSpan.FromSeconds(5), token);
            }
        }
    }
}