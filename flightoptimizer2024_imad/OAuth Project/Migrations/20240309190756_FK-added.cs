using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OAuth_Project.Migrations
{
    public partial class FKadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BookingId",
                table: "FlightSeats",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_FlightSeats_BookingId",
                table: "FlightSeats",
                column: "BookingId");

            migrationBuilder.AddForeignKey(
                name: "FK_FlightSeats_Bookings_BookingId",
                table: "FlightSeats",
                column: "BookingId",
                principalTable: "Bookings",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FlightSeats_Bookings_BookingId",
                table: "FlightSeats");

            migrationBuilder.DropIndex(
                name: "IX_FlightSeats_BookingId",
                table: "FlightSeats");

            migrationBuilder.DropColumn(
                name: "BookingId",
                table: "FlightSeats");
        }
    }
}
