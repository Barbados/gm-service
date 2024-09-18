using Ardalis.SharedKernel;
using Gm.Domain.Aggregates.SubscriberAggregate;
using Gm.Domain.Aggregates.SubscriptionAggregate;
using Gm.Domain.Aggregates.SubscriptionAggregate.Specifications;

namespace Gm.Application.Subscribers.Queries;

public class GetSubscribersByTopicQueryHandler(IRepository<Subscription> repository) : IQueryHandler<GetSubscribersByTopicQuery, List<Subscriber>>
{
    public async Task<List<Subscriber>> Handle(GetSubscribersByTopicQuery request, CancellationToken cancellationToken)
    {
        var subscriptions = await repository.ListAsync(new GetSubscriptionsByTopicSpec(request.Topic), cancellationToken);

        return subscriptions.Select(s => s.Subscriber).ToList()!;
    }
}