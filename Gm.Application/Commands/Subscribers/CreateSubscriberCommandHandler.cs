using Ardalis.SharedKernel;
using Gm.Domain.Aggregates.SubscriberAggregate;
using MediatR;

namespace Gm.Application.Commands.Subscribers;

public class CreateSubscriberCommandHandler(IRepository<Subscriber> repository) : IRequestHandler<CreateSubscriberCommand>
{
    public async Task Handle(CreateSubscriberCommand request, CancellationToken cancellationToken)
    {
        var newSubscriber = new Subscriber
        {
            TgChatId = request.ChatId
        };
        newSubscriber.AddSubscription(request.Topic);

        await repository.AddAsync(newSubscriber, cancellationToken);
    }
}