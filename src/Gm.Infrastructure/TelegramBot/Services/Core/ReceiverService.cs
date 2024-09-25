using Gm.Infrastructure.TelegramBot.Abstract;
using Gm.Infrastructure.TelegramBot.Common;
using Microsoft.Extensions.Logging;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types.Enums;

namespace Gm.Infrastructure.TelegramBot.Services.Core;

public class ReceiverService(
    ITelegramBotClient botClient,
    IUpdateHandler updateHandler,
    ILogger<ReceiverService> logger)
    : IReceiverService
{
    public async Task ReceiveAsync(CancellationToken token)
    {
        var receiverOptions = new ReceiverOptions
        {
            AllowedUpdates = Array.Empty<UpdateType>(),
            DropPendingUpdates = true
        };

        await botClient.GetMeAsync(token);

        var commands = BotCommandHelper.GetAllCommands();
        await botClient.SetMyCommandsAsync(commands, cancellationToken: token);
        await botClient.ReceiveAsync(
            updateHandler: updateHandler,
            receiverOptions: receiverOptions,
            cancellationToken: token);
    }
}