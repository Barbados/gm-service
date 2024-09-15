namespace Gm.Infrastructure.TelegramBot.Abstract;

public interface ISenderService
{
    Task SendAsync(CancellationToken token);
}