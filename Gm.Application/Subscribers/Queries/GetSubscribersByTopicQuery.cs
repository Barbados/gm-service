using Ardalis.SharedKernel;
using Gm.Domain.Aggregates.SubscriberAggregate;
using Gm.Domain.Aggregates.SubscriptionAggregate;

namespace Gm.Application.Subscribers.Queries;

public record GetSubscribersByTopicQuery(SubscriptionTopic Topic) : IQuery<List<Subscriber>>;
