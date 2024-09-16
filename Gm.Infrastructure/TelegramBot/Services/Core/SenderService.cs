using Gm.Infrastructure.TelegramBot.Abstract;
using Telegram.Bot;

namespace Gm.Infrastructure.TelegramBot.Services.Core;

public class SenderService : ISenderService
{
    // temporary hardcoded, just to test initial functionality
    private const long ChatId = 789429780;
    private readonly ITelegramBotClient _botClient;
    
    public SenderService(ITelegramBotClient botClient)
    {
        _botClient = botClient;
    }
    
    public async Task SendAsync(CancellationToken token)
    {
        await _botClient.GetMeAsync(token);

        await _botClient.SendTextMessageAsync(ChatId, "Good Night!", cancellationToken: token);
    }
}