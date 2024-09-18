using Ardalis.Specification;

namespace Gm.Domain.Aggregates.SubscriberAggregate.Specifications;

public sealed class GetSubscriberByChatIdSpec : SingleResultSpecification<Subscriber>
{
    public GetSubscriberByChatIdSpec(long chatId)
    {
        Query.Where(s => s.TgChatId == chatId);
    }
}