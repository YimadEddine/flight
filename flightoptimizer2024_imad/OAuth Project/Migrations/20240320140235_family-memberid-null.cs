using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OAuth_Project.Migrations
{
    public partial class familymemberidnull : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "age",
                table: "Passengers",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "MemberId",
                table: "Families",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "age",
                table: "Passengers");

            migrationBuilder.AlterColumn<int>(
                name: "MemberId",
                table: "Families",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }
    }
}
