using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Projektor.Infrastructure.Migrations
{
    public partial class AddIssueStatusAndResolution : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ClosedAt",
                table: "Issues",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ClosedById",
                table: "Issues",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Resolution",
                table: "Issues",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Issues_ClosedAt",
                table: "Issues",
                column: "ClosedAt");

            migrationBuilder.CreateIndex(
                name: "IX_Issues_ClosedById",
                table: "Issues",
                column: "ClosedById");

            migrationBuilder.CreateIndex(
                name: "IX_Issues_Resolution",
                table: "Issues",
                column: "Resolution");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Issues_ClosedAt",
                table: "Issues");

            migrationBuilder.DropIndex(
                name: "IX_Issues_ClosedById",
                table: "Issues");

            migrationBuilder.DropIndex(
                name: "IX_Issues_Resolution",
                table: "Issues");

            migrationBuilder.DropColumn(
                name: "ClosedAt",
                table: "Issues");

            migrationBuilder.DropColumn(
                name: "ClosedById",
                table: "Issues");

            migrationBuilder.DropColumn(
                name: "Resolution",
                table: "Issues");
        }
    }
}
