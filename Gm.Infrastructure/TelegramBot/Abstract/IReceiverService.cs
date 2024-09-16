namespace Gm.Infrastructure.TelegramBot.Abstract;

public interface IReceiverService
{
    Task ReceiveAsync(CancellationToken token);
}