using Gm.Domain.Aggregates.SubscriptionAggregate;
using MediatR;

namespace Gm.Application.Subscribers.Commands;

public record CreateSubscriberCommand(long ChatId, SubscriptionTopic Topic) : IRequest;
