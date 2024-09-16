using Gm.Domain.Aggregates.SeedWork;

namespace Gm.Domain.Aggregates.SubscriptionAggregate;

public class Subscription : EntityBase
{
    public Guid AccountId { get; set; }
    public string? SubscriptionSchedule { get; set; }
}