using Ardalis.SharedKernel;
using Gm.Domain.Aggregates.SubscriptionAggregate;
using EntityBase = Gm.Domain.Aggregates.SeedWork.EntityBase;

namespace Gm.Domain.Aggregates.SubscriberAggregate;

public class Subscriber : EntityBase, IAggregateRoot
{
    public long TgChatId { get; init; }

    private readonly List<Subscription> _subscriptions = new();
    public IEnumerable<Subscription> Subscriptions => _subscriptions.AsReadOnly();

    public void AddSubscription(SubscriptionTopic topic)
    {
        _subscriptions.Add(new Subscription
        {
            Topic = topic
        });
    }
}