using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OAuth_Project.Migrations
{
    public partial class bookingseatpassengeraddedtoflightseat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SeatPassengerId",
                table: "FlightSeats",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_FlightSeats_SeatPassengerId",
                table: "FlightSeats",
                column: "SeatPassengerId");

            migrationBuilder.AddForeignKey(
                name: "FK_FlightSeats_Passengers_SeatPassengerId",
                table: "FlightSeats",
                column: "SeatPassengerId",
                principalTable: "Passengers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FlightSeats_Passengers_SeatPassengerId",
                table: "FlightSeats");

            migrationBuilder.DropIndex(
                name: "IX_FlightSeats_SeatPassengerId",
                table: "FlightSeats");

            migrationBuilder.DropColumn(
                name: "SeatPassengerId",
                table: "FlightSeats");
        }
    }
}
