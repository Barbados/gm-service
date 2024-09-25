using Ardalis.SharedKernel;
using Gm.Domain.Aggregates.SubscriberAggregate;
using Gm.Domain.Aggregates.SubscriptionAggregate;

namespace Gm.Application.UseCases.Subscribers.Queries;

public record GetSubscribersByTopicQuery(SubscriptionTopic Topic) : IQuery<List<Subscriber>>;
