using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Projektor.Infrastructure.Migrations
{
    public partial class AddIssuePriority : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Priority",
                table: "Issues",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Issues_Priority",
                table: "Issues",
                column: "Priority");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Issues_Priority",
                table: "Issues");

            migrationBuilder.DropColumn(
                name: "Priority",
                table: "Issues");
        }
    }
}
