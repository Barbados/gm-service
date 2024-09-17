using Gm.Domain.Aggregates.SeedWork;
using Gm.Domain.Aggregates.TgChatAggregate;

namespace Gm.Domain.Aggregates.SubscriptionAggregate;

public class Subscription : EntityBase
{
    public Guid SubscriptionId { get; set; }
    public Subscriber? Subscriber { get; set; }
    public string? SubscriptionSchedule { get; set; }
    public bool IsActive { get; set; } = true;
    public SubscriptionTopic Topic { get; set; } = SubscriptionTopic.GoodMorning;
}