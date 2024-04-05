using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OAuth_Project.Migrations
{
    public partial class SeatNameAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SeatName",
                table: "FlightSeats",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SeatName",
                table: "FlightSeats");
        }
    }
}
