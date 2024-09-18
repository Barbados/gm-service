using Gm.Domain.Aggregates.SubscriptionAggregate;
using MediatR;

namespace Gm.Application.Commands.Subscribers;

public record CreateSubscriberCommand(long ChatId, SubscriptionTopic Topic) : IRequest;
