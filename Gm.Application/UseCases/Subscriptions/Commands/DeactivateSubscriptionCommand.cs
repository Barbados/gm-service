using Gm.Domain.Aggregates.SubscriptionAggregate;
using MediatR;

namespace Gm.Application.UseCases.Subscriptions.Commands;

public sealed record DeactivateSubscriptionCommand(long ChatId, SubscriptionTopic Topic) : IRequest<bool>;
