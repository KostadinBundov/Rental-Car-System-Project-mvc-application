using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rental_Car_System_Project.Migrations
{
    public partial class AddApproveProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsRequestApproved",
                table: "Requests",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsRequestApproved",
                table: "Requests");
        }
    }
}
