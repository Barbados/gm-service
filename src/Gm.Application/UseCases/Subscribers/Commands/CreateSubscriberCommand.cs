using Gm.Domain.Aggregates.SubscriptionAggregate;
using MediatR;

namespace Gm.Application.UseCases.Subscribers.Commands;

public record CreateSubscriberCommand(long ChatId, SubscriptionTopic Topic) : IRequest;
