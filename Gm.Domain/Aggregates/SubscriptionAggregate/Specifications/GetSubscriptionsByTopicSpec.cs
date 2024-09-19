using Ardalis.Specification;

namespace Gm.Domain.Aggregates.SubscriptionAggregate.Specifications;

public sealed class GetSubscriptionsByTopicSpec : Specification<Subscription>
{
    public GetSubscriptionsByTopicSpec(SubscriptionTopic topic)
    {
        Query.Where(s => s.Topic == topic && s.IsActive)
            .Include(s => s.Subscriber);
    }
}