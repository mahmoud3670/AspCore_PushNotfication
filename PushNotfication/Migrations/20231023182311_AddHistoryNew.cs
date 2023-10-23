using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PushNotfication.Migrations
{
    /// <inheritdoc />
    public partial class AddHistoryNew : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NotificationHistory_Users_UserId",
                table: "NotificationHistory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NotificationHistory",
                table: "NotificationHistory");

            migrationBuilder.RenameTable(
                name: "NotificationHistory",
                newName: "NotificationHistories");

            migrationBuilder.RenameIndex(
                name: "IX_NotificationHistory_UserId",
                table: "NotificationHistories",
                newName: "IX_NotificationHistories_UserId");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "NotificationHistories",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_NotificationHistories",
                table: "NotificationHistories",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_NotificationHistories_Users_UserId",
                table: "NotificationHistories",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NotificationHistories_Users_UserId",
                table: "NotificationHistories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NotificationHistories",
                table: "NotificationHistories");

            migrationBuilder.RenameTable(
                name: "NotificationHistories",
                newName: "NotificationHistory");

            migrationBuilder.RenameIndex(
                name: "IX_NotificationHistories_UserId",
                table: "NotificationHistory",
                newName: "IX_NotificationHistory_UserId");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "NotificationHistory",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_NotificationHistory",
                table: "NotificationHistory",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_NotificationHistory_Users_UserId",
                table: "NotificationHistory",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
