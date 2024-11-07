using Gm.Infrastructure.TelegramBot.Abstract;
using Gm.Infrastructure.TelegramBot.Common;
using Microsoft.Extensions.Logging;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Gm.Infrastructure.TelegramBot.Services.Core;

public class ReceiverService(
    ITelegramBotClient botClient,
    IUpdateHandler updateHandler,
    ILogger<ReceiverService> logger)
    : IReceiverService
{
    private static readonly long AdminChatId = 789429780;
    
    public async Task ReceiveAsync(CancellationToken token)
    {
        var receiverOptions = new ReceiverOptions
        {
            AllowedUpdates = Array.Empty<UpdateType>(),
            DropPendingUpdates = true
        };

        var currentUser = await botClient.GetMeAsync(token);
        var commands = BotCommandHelper.GetAllCommands();

        // Add admin commands
        if (currentUser.Id != AdminChatId)
        {
            var adminCommands = BotCommandHelper.GetAdminCommands();
            await botClient.SetMyCommandsAsync(commands.Concat(adminCommands), BotCommandScope.Chat(AdminChatId),
                cancellationToken: token);
        }
        else
        {
            await botClient.SetMyCommandsAsync(commands, cancellationToken: token);
        }

        await botClient.ReceiveAsync(
            updateHandler: updateHandler,
            receiverOptions: receiverOptions,
            cancellationToken: token);
    }
}