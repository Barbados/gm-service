using Ardalis.SmartEnum;

namespace Gm.Domain.Aggregates.SubscriptionAggregate;

public sealed class SubscriptionTopic : SmartEnum<SubscriptionTopic, string>
{
    public static readonly SubscriptionTopic GoodMorning = new(nameof(GoodMorning), "good_morning");
    
    private SubscriptionTopic(string name, string value) : base(name, value)
    {
    }
}