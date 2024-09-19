using Gm.Domain.Aggregates.SubscriptionAggregate;
using MediatR;

namespace Gm.Application.UseCases.Subscriptions.Queries;

public sealed record GetSubscriptionsByTopicQuery(SubscriptionTopic Topic) : IRequest<List<SubscriptionDto>>;
