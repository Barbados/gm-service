using Ardalis.SharedKernel;
using Gm.Domain.Aggregates.SubscriptionAggregate;
using Gm.Domain.Aggregates.SubscriptionAggregate.Specifications;
using MediatR;

namespace Gm.Application.UseCases.Subscriptions.Queries;

public class GetSubscriptionsByTopicQueryHandler(IRepository<Subscription> repository) : IRequestHandler<GetSubscriptionsByTopicQuery, List<SubscriptionDto>>
{
    public async Task<List<SubscriptionDto>> Handle(GetSubscriptionsByTopicQuery request, CancellationToken cancellationToken)
    {
        var subscriptions = await repository.ListAsync(new GetSubscriptionsByTopicSpec(request.Topic), cancellationToken);

        var subscriptionsDto = new List<SubscriptionDto>();
        foreach (var subscription in subscriptions)
        {
            if (subscription is { Subscriber: not null })
                    subscriptionsDto.Add(new SubscriptionDto(subscription.Subscriber.TgChatId,
                        subscription.SubscriptionSchedule));
        }

        return subscriptionsDto;
    }
}