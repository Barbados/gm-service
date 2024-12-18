﻿// <auto-generated />
using System;
using Gm.Infrastructure.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Gm.Infrastructure.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240924122209_UpdSubscriptionEntity")]
    partial class UpdSubscriptionEntity
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Gm.Domain.Aggregates.SubscriberAggregate.Subscriber", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<long>("TgChatId")
                        .HasColumnType("bigint")
                        .HasColumnName("tg_chat_id");

                    b.HasKey("Id")
                        .HasName("pk_subscribers");

                    b.ToTable("subscribers", (string)null);
                });

            modelBuilder.Entity("Gm.Domain.Aggregates.SubscriptionAggregate.Subscription", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean")
                        .HasColumnName("is_active");

                    b.Property<Guid>("SubscriberId")
                        .HasColumnType("uuid")
                        .HasColumnName("subscriber_id");

                    b.Property<string>("SubscriptionSchedule")
                        .HasColumnType("text")
                        .HasColumnName("subscription_schedule");

                    b.Property<string>("Topic")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("topic");

                    b.HasKey("Id")
                        .HasName("pk_subscriptions");

                    b.HasIndex("SubscriberId")
                        .HasDatabaseName("ix_subscriptions_subscriber_id");

                    b.HasIndex("Topic")
                        .HasDatabaseName("ix_subscriptions_topic");

                    b.ToTable("subscriptions", (string)null);
                });

            modelBuilder.Entity("Gm.Domain.Aggregates.SubscriptionAggregate.Subscription", b =>
                {
                    b.HasOne("Gm.Domain.Aggregates.SubscriberAggregate.Subscriber", "Subscriber")
                        .WithMany("Subscriptions")
                        .HasForeignKey("SubscriberId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_subscriptions_subscribers_subscriber_id");

                    b.Navigation("Subscriber");
                });

            modelBuilder.Entity("Gm.Domain.Aggregates.SubscriberAggregate.Subscriber", b =>
                {
                    b.Navigation("Subscriptions");
                });
#pragma warning restore 612, 618
        }
    }
}
