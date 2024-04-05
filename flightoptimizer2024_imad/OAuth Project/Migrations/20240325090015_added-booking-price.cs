using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OAuth_Project.Migrations
{
    public partial class addedbookingprice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "totalPrice",
                table: "Bookings",
                type: "real",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "totalPrice",
                table: "Bookings");
        }
    }
}
