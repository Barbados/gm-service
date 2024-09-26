using Gm.Domain.Aggregates.SubscriptionAggregate;
using Gm.Infrastructure.TelegramBot.Abstract;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Gm.Infrastructure.TelegramBot.Services.Core;

public class SenderService(ITelegramBotClient botClient) : ISenderService
{
    public async Task<Message> SendAsync(long chatId, SubscriptionTopic topic, CancellationToken token)
    {
        var sentMessage = await (topic.Name switch
        {
            nameof(SubscriptionTopic.GoodMorning) => botClient.SendTextMessageAsync(chatId, ComposeGmMessage(), cancellationToken: token),
            _ => throw new ArgumentOutOfRangeException()
        });

        return sentMessage;
    }

    private static string ComposeGmMessage()
    {
        var message = $"Hello!\nToday is {DateTime.Today:dddd}, {DateTime.Today:dd-MM-yyyy}.";
        message += $"\nHave a nice day! See you tomorrow.";

        return message;
    }
}