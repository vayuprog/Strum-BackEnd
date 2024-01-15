using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Strum.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class posts1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PostImage",
                table: "Post");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "PostImage",
                table: "Post",
                type: "bytea",
                nullable: true);
        }
    }
}
