using Gm.Domain.Aggregates.SubscriptionAggregate;

namespace Gm.Infrastructure.TelegramBot.Abstract;

public interface ISenderService
{
    Task SendAsync(long chatId, SubscriptionTopic topic, CancellationToken token);
}