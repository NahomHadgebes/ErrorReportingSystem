using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ErrorReportingSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddCreatedByUsername : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AssignedToUserId",
                table: "ErrorReports");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "ErrorReports");

            migrationBuilder.RenameColumn(
                name: "EquipmentId",
                table: "ErrorReports",
                newName: "CreatedByUsername");

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ErrorReportId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.RenameColumn(
                name: "CreatedByUsername",
                table: "ErrorReports",
                newName: "EquipmentId");

            migrationBuilder.AddColumn<Guid>(
                name: "AssignedToUserId",
                table: "ErrorReports",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserId",
                table: "ErrorReports",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }
    }
}
