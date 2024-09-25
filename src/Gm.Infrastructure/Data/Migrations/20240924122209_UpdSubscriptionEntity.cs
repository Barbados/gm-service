using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gm.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdSubscriptionEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_subscriptions_subscribers_subscription_id",
                table: "subscriptions");

            migrationBuilder.RenameColumn(
                name: "subscription_id",
                table: "subscriptions",
                newName: "subscriber_id");

            migrationBuilder.RenameIndex(
                name: "ix_subscriptions_subscription_id",
                table: "subscriptions",
                newName: "ix_subscriptions_subscriber_id");

            migrationBuilder.AddForeignKey(
                name: "fk_subscriptions_subscribers_subscriber_id",
                table: "subscriptions",
                column: "subscriber_id",
                principalTable: "subscribers",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_subscriptions_subscribers_subscriber_id",
                table: "subscriptions");

            migrationBuilder.RenameColumn(
                name: "subscriber_id",
                table: "subscriptions",
                newName: "subscription_id");

            migrationBuilder.RenameIndex(
                name: "ix_subscriptions_subscriber_id",
                table: "subscriptions",
                newName: "ix_subscriptions_subscription_id");

            migrationBuilder.AddForeignKey(
                name: "fk_subscriptions_subscribers_subscription_id",
                table: "subscriptions",
                column: "subscription_id",
                principalTable: "subscribers",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
