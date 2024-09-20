using Gm.Domain.Aggregates.SubscriptionAggregate;
using Telegram.Bot.Types;

namespace Gm.Infrastructure.TelegramBot.Abstract;

public interface ISenderService
{
    Task<Message> SendAsync(long chatId, SubscriptionTopic topic, CancellationToken token);
}