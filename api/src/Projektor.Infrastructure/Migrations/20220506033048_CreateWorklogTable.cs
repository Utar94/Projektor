using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Projektor.Infrastructure.Migrations
{
    public partial class CreateWorklogTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Worklogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Description = table.Column<string>(type: "text", nullable: true),
                    EndedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IssueId = table.Column<int>(type: "integer", nullable: false),
                    StartedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    CreatedById = table.Column<Guid>(type: "uuid", nullable: false),
                    Deleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedById = table.Column<Guid>(type: "uuid", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedById = table.Column<Guid>(type: "uuid", nullable: true),
                    Uuid = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    Version = table.Column<int>(type: "integer", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Worklogs", x => x.Id);
                    table.CheckConstraint("CHK_Worklogs_StartedEnded", "\"StartedAt\" < \"EndedAt\"");
                    table.ForeignKey(
                        name: "FK_Worklogs_Issues_IssueId",
                        column: x => x.IssueId,
                        principalTable: "Issues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Worklogs_CreatedById",
                table: "Worklogs",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Worklogs_Deleted",
                table: "Worklogs",
                column: "Deleted");

            migrationBuilder.CreateIndex(
                name: "IX_Worklogs_DeletedById",
                table: "Worklogs",
                column: "DeletedById");

            migrationBuilder.CreateIndex(
                name: "IX_Worklogs_IssueId",
                table: "Worklogs",
                column: "IssueId");

            migrationBuilder.CreateIndex(
                name: "IX_Worklogs_UpdatedById",
                table: "Worklogs",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Worklogs_Uuid",
                table: "Worklogs",
                column: "Uuid",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Worklogs");
        }
    }
}
