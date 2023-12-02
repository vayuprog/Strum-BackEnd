using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Strum.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Second : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Users",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Users",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<int>(
                name: "MessagesId",
                table: "Users",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MusicianId",
                table: "Users",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Message",
                table: "Messages",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "Time",
                table: "Messages",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "Musicians",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Ganre = table.Column<string>(type: "text", nullable: false),
                    Expirience = table.Column<string>(type: "text", nullable: false),
                    Instrument = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Musicians", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_MessagesId",
                table: "Users",
                column: "MessagesId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_MusicianId",
                table: "Users",
                column: "MusicianId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Messages_MessagesId",
                table: "Users",
                column: "MessagesId",
                principalTable: "Messages",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Musicians_MusicianId",
                table: "Users",
                column: "MusicianId",
                principalTable: "Musicians",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Messages_MessagesId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Musicians_MusicianId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "Musicians");

            migrationBuilder.DropIndex(
                name: "IX_Users_MessagesId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_MusicianId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "MessagesId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "MusicianId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Message",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "Time",
                table: "Messages");

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Users",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Users",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);
        }
    }
}
