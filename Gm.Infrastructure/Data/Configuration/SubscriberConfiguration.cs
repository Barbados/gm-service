using Gm.Domain.Aggregates.SubscriberAggregate;
using Gm.Domain.Aggregates.SubscriptionAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gm.Infrastructure.Data.Configuration;

public class SubscriberConfiguration : IEntityTypeConfiguration<Subscriber>
{
    public void Configure(EntityTypeBuilder<Subscriber> builder)
    {
        builder.ToTable("subscribers");

        builder.HasMany(s => s.Subscriptions)
            .WithOne(subs => subs.Subscriber)
            .HasForeignKey(s => s.SubscriptionId);
    }
}

public class SubscriptionConfiguration : IEntityTypeConfiguration<Subscription>
{
    public void Configure(EntityTypeBuilder<Subscription> builder)
    {
        builder.ToTable("subscriptions");

        builder.Property(s => s.Topic)
            .HasConversion(s => s.Value,
                s => SubscriptionTopic.FromValue(s));
        
        builder.HasIndex(s => s.Topic);
    }
}