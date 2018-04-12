using Microsoft.EntityFrameworkCore.Migrations;

namespace EventOnFly.Migrations
{
    public partial class AddServiceState : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "State",
                table: "Service",
                nullable: false,
                defaultValue: 2);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "State",
                table: "Service");
        }
    }
}
