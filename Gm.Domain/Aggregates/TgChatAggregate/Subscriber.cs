using Gm.Domain.Aggregates.SeedWork;
using Gm.Domain.Aggregates.SubscriptionAggregate;

namespace Gm.Domain.Aggregates.TgChatAggregate;

public class Subscriber : EntityBase
{
    public long TgChatId { get; set; }
    public IEnumerable<Subscription>? Subscriptions { get; set; }
}