using Gm.Domain.Aggregates.SubscriptionAggregate;
using Gm.Infrastructure.TelegramBot.Abstract;
using Telegram.Bot;

namespace Gm.Infrastructure.TelegramBot.Services.Core;

public class SenderService(ITelegramBotClient botClient) : ISenderService
{
    public async Task SendAsync(long chatId, SubscriptionTopic topic, CancellationToken token)
    {
        switch (topic.Name)
        {
            case nameof(SubscriptionTopic.GoodMorning):
                await botClient.SendTextMessageAsync(chatId, "Good Night!", cancellationToken: token);
                break;
        }
    }
}