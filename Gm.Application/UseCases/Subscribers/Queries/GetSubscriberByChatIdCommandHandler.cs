using Ardalis.SharedKernel;
using Gm.Domain.Aggregates.SubscriberAggregate;
using Gm.Domain.Aggregates.SubscriberAggregate.Specifications;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Gm.Application.UseCases.Subscribers.Queries;

public class GetSubscriberByChatIdCommandHandler(IRepository<Subscriber> repository,
    ILogger<GetSubscriberByChatIdCommandHandler> logger) : IRequestHandler<GetSubscriberByChatIdCommand, Subscriber?>
{
    public async Task<Subscriber?> Handle(GetSubscriberByChatIdCommand request, CancellationToken cancellationToken)
    {
        var existingSubscriber = await repository.SingleOrDefaultAsync(new GetSubscriberByChatIdSpec(request.ChatId), cancellationToken);

        if (existingSubscriber is not null)
        {
            logger.LogInformation($"Tg chat with id {request.ChatId} has already registered.");
            
        }
        
        return existingSubscriber;
    }
}