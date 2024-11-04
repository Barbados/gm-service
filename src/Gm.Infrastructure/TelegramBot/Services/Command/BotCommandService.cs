using Gm.Application.UseCases.Subscribers.Commands;
using Gm.Application.UseCases.Subscribers.Queries;
using Gm.Application.UseCases.Subscriptions.Commands;
using Gm.Domain.Aggregates.SubscriptionAggregate;
using Gm.Infrastructure.TelegramBot.Abstract;
using Gm.Infrastructure.TelegramBot.Model;
using MediatR;
using Microsoft.Extensions.Logging;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace Gm.Infrastructure.TelegramBot.Services.Command;

public class BotCommandService(
    ISenderService senderService,
    ITelegramBotClient botClient,
    IMediator mediator,
    ILogger<BotCommandService> logger) : IBotCommandService
{
    public async Task HandleAsync(Message message)
    {
        try
        {
            if (message.Text is not { } messageText)
                return;

            var command = ExtractCommand(messageText);
            BotCommandType.TryFromValue(command, out var commandType);
            var chatId = message.Chat.Id;
            // Handle command selected
            if (commandType is not null)
            {
                var sentMessage = await (commandType.Name switch
                {
                    nameof(BotCommandType.Start) => StartConversation(message),
                    nameof(BotCommandType.Subscribe) => Subscribe(message),
                    nameof(BotCommandType.Unsubscribe) => Unsubscribe(message),
                    nameof(BotCommandType.Test) => Test(message),
                    _ => Usage(message)
                });
                logger.LogInformation($"The message was sent with id: {sentMessage.MessageId}");
            }

        }
        catch (Exception e)
        {
            logger.LogError($"An error occured while handling the message: {e.Message}");
            throw;
        }
    }

    private string ExtractCommand(string messageText)
    {
        var isSentInGroupChat = messageText.Contains($"@gmmebot"); // todo: move to appsettings.json or get name in runtime
        messageText = messageText.Split(isSentInGroupChat ? '@' : ' ')[0];
        
        return messageText;
    }

    private async Task<Message> StartConversation(Message message)
    {
        return await botClient.SendTextMessageAsync(message.Chat.Id,
            $"Hello Dear {message.From!.Username}! Welcome to GM bot!");
    }

    private async Task<Message> Subscribe(Message message)
    {
        var msg = $"Congratulations! You've just subscribed for getting GM messages daily.";
        var existingSubscriber = await mediator.Send(new GetSubscriberByChatIdCommand(message.Chat.Id));
        if (existingSubscriber is not null)
            msg = "Good news, you are already subscribed.";
        else
            await mediator.Send(new CreateSubscriberCommand(message.Chat.Id, SubscriptionTopic.GoodMorning));
        
        return await botClient.SendTextMessageAsync(message.Chat.Id, msg);
    }
    
    private async Task<Message> Unsubscribe(Message message)
    {
        await mediator.Send(new DeactivateSubscriptionCommand(message.Chat.Id, SubscriptionTopic.GoodMorning));
        const string msg = $"Unfortunately, you've just unsubscribed from getting GM messages. We wish you come back soon!";

        return await botClient.SendTextMessageAsync(message.Chat.Id, msg);
    }

    private async Task<Message> Test(Message message)
    {
        return await senderService.SendAsync(message.Chat.Id, SubscriptionTopic.GoodMorning, default);
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