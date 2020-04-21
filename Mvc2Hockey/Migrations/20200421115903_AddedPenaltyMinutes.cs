using Microsoft.EntityFrameworkCore.Migrations;

namespace Mvc2Hockey.Migrations
{
    public partial class AddedPenaltyMinutes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PenaltyMinutes",
                table: "HistoryRecords",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PenaltyMinutes",
                table: "HistoryRecords");
        }
    }
}
