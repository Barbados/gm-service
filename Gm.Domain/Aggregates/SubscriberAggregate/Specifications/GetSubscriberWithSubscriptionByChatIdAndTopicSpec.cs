using Ardalis.Specification;
using Gm.Domain.Aggregates.SubscriptionAggregate;

namespace Gm.Domain.Aggregates.SubscriberAggregate.Specifications;

public sealed class GetSubscriberWithSubscriptionByChatIdAndTopicSpec : SingleResultSpecification<Subscriber>
{
    public GetSubscriberWithSubscriptionByChatIdAndTopicSpec(long chatId, SubscriptionTopic topic)
    {
        Query.Where(s => s.TgChatId == chatId)
            .Include(s => s.Subscriptions.Where(sub => sub.Topic == topic && sub.IsActive));
    }
}