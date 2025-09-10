using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ErrorReportingSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddCommentModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Comments");

            migrationBuilder.RenameColumn(
                name: "Message",
                table: "Comments",
                newName: "CreatedByUsername");

            migrationBuilder.AddColumn<string>(
                name: "Content",
                table: "Comments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_ErrorReportId",
                table: "Comments",
                column: "ErrorReportId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_ErrorReports_ErrorReportId",
                table: "Comments",
                column: "ErrorReportId",
                principalTable: "ErrorReports",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_ErrorReports_ErrorReportId",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_ErrorReportId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "Content",
                table: "Comments");

            migrationBuilder.RenameColumn(
                name: "CreatedByUsername",
                table: "Comments",
                newName: "Message");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Comments",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }
    }
}
