using Ardalis.SharedKernel;
using Gm.Domain.Aggregates.SubscriberAggregate;
using Gm.Domain.Aggregates.SubscriberAggregate.Specifications;
using MediatR;

namespace Gm.Application.UseCases.Subscriptions.Commands;

public class DeactivateSubscriptionCommandHandler(IRepository<Subscriber> repository) : IRequestHandler<DeactivateSubscriptionCommand, bool>
{
    public async Task<bool> Handle(DeactivateSubscriptionCommand request, CancellationToken cancellationToken)
    {
        var subscriber = await repository.SingleOrDefaultAsync(
            new GetSubscriberWithSubscriptionByChatIdAndTopicSpec(request.ChatId, request.Topic), cancellationToken);
        
        if (subscriber is null)
            return false;
        
        subscriber.Unsubscribe(request.Topic);

        await repository.UpdateAsync(subscriber, cancellationToken);

        return true;
    }
}