using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Strum.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Security : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Messages_MessagesId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_MessagesId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "MessagesId",
                table: "Users");

            migrationBuilder.AddColumn<string>(
                name: "Salt",
                table: "Users",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Salt",
                table: "Users");

            migrationBuilder.AddColumn<int>(
                name: "MessagesId",
                table: "Users",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_MessagesId",
                table: "Users",
                column: "MessagesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Messages_MessagesId",
                table: "Users",
                column: "MessagesId",
                principalTable: "Messages",
                principalColumn: "Id");
        }
    }
}
