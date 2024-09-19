using Ardalis.SharedKernel;
using Gm.Domain.Aggregates.SubscriberAggregate;
using EntityBase = Gm.Domain.Aggregates.SeedWork.EntityBase;

namespace Gm.Domain.Aggregates.SubscriptionAggregate;

public class Subscription : EntityBase, IAggregateRoot
{
    public Guid SubscriptionId { get; set; }
    
    public Subscriber? Subscriber { get; set; }
    
    // This is for future needs
    public string? SubscriptionSchedule { get; set; }
    
    public bool IsActive { get; set; } = true;
    
    public SubscriptionTopic Topic { get; set; } = SubscriptionTopic.GoodMorning;
}