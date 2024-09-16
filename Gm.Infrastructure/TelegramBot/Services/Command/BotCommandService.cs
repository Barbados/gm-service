using Gm.Infrastructure.TelegramBot.Abstract;
using Gm.Infrastructure.TelegramBot.Model;
using Microsoft.Extensions.Logging;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace Gm.Infrastructure.TelegramBot.Services.Command;

public class BotCommandService(
    ITelegramBotClient botClient,
    ILogger<BotCommandService> logger) : IBotCommandService
{
    public async Task HandleAsync(Message message)
    {
        if (message.Text is not { } messageText)
            return;

        var command = messageText.Split(' ')[0];
        BotCommandType.TryFromValue(command, out var commandType);
        var chatId = message.Chat.Id;
        // Handle command selected
        if (commandType is not null)
        {
            var sentMessage = await (commandType.Name switch
            {
                nameof(BotCommandType.Start) => StartConversation(message),
                nameof(BotCommandType.Register) => SendText(message),
                _ => Usage(message)
            });
            logger.LogInformation($"The message was sent with id: {sentMessage.MessageId}");
        }
    }

    private async Task<Message> StartConversation(Message message)
    {
        var replyMarkup = new ReplyKeyboardMarkup(true)
            .AddButtons("Loose weight", "Gain weight", "Other");

        return await botClient.SendTextMessageAsync(message.Chat.Id,
            $"Hello Dear {message.From!.Username}! Welcome to Donny bot! Please let me know how I can help." +
            $"\n\nSelect from the options or just text:",
            replyMarkup: replyMarkup);
    }

    private async Task<Message> Usage(Message msg)
    {
        const string usage = "Unrecognized command. Say what?";
        return await botClient.SendTextMessageAsync(msg.Chat,
            usage,
            parseMode: ParseMode.Html,
            replyMarkup: new ReplyKeyboardRemove());
    }

    private async Task<Message> SendText(Message msg)
    {
        logger.LogInformation($"Received a text {msg.Text} in chat {msg.Chat.Id}...");
        return await botClient.SendTextMessageAsync(msg.Chat, $"{msg.From} said: {msg.Text}");
    }
}