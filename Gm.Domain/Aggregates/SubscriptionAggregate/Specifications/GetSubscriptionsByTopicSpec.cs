using Ardalis.Specification;

namespace Gm.Domain.Aggregates.SubscriptionAggregate.Specifications;

public sealed class GetSubscriptionsByTopicSpec : Specification<Subscription>
{
    public GetSubscriptionsByTopicSpec(SubscriptionTopic topic)
    {
        Query.Where(s => s.Topic == topic)
            .Include(s => s.Subscriber);
    }
}