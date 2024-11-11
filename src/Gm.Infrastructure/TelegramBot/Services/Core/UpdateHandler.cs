using Gm.Infrastructure.TelegramBot.Abstract;
using Microsoft.Extensions.Logging;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;

namespace Gm.Infrastructure.TelegramBot.Services.Core;

public class UpdateHandler(IBotCommandService commandService,
    IBotConversationService conversationService,
    ILogger<UpdateHandler> logger) : IUpdateHandler
{
    public async Task HandleErrorAsync(ITelegramBotClient bot, Exception exception, HandleErrorSource source,
        CancellationToken cancellationToken)
    {
        if (exception is RequestException)
            await Task.Delay(TimeSpan.FromSeconds(5), cancellationToken);
    }

    public async Task HandleUpdateAsync(ITelegramBotClient bot, Update update,
        CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        await (update switch
        {
            { Message: { } message } => OnMessage(message),
            _ => UnknownUpdateHandlerAsync(update)
        });
    }

    private async Task OnMessage(Message message)
    {
        // Handle command selected
        await commandService.HandleAsync(message);
        
        // Handle conversation in case if any command initiates it
        await conversationService.HandleAsync(message);
    }
    
    private Task UnknownUpdateHandlerAsync(Update update)
    {
        return Task.CompletedTask;
    }
}