using Ardalis.SharedKernel;
using Gm.Domain.Aggregates.SubscriberAggregate;
using EntityBase = Gm.Domain.Aggregates.SeedWork.EntityBase;

namespace Gm.Domain.Aggregates.SubscriptionAggregate;

public class Subscription : EntityBase, IAggregateRoot
{
    public Guid SubscriptionId { get; init; }
    
    public Subscriber? Subscriber { get; init; }
    
    // This is for future needs
    public string? SubscriptionSchedule { get; init; }
    
    public bool IsActive { get; private set; } = true;
    
    public SubscriptionTopic Topic { get; init; } = SubscriptionTopic.GoodMorning;

    public void Deactivate()
    {
        IsActive = false;
    }
}