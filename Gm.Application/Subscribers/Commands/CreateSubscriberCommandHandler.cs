using Ardalis.SharedKernel;
using Gm.Domain.Aggregates.SubscriberAggregate;
using Gm.Domain.Aggregates.SubscriberAggregate.Specifications;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Gm.Application.Subscribers.Commands;

public class CreateSubscriberCommandHandler(IRepository<Subscriber> repository,
    ILogger<CreateSubscriberCommandHandler> logger) : IRequestHandler<CreateSubscriberCommand>
{
    public async Task Handle(CreateSubscriberCommand request, CancellationToken cancellationToken)
    {
        var existingSubscriber =
            await repository.SingleOrDefaultAsync(new GetSubscriberByChatIdSpec(request.ChatId), cancellationToken);

        if (existingSubscriber is not null)
        {
            logger.LogInformation($"Tg chat with id {request.ChatId} has already registered.");
            return;
        }
        
        var newSubscriber = new Subscriber
        {
            TgChatId = request.ChatId
        };
        newSubscriber.AddSubscription(request.Topic);

        await repository.AddAsync(newSubscriber, cancellationToken);
    }
}