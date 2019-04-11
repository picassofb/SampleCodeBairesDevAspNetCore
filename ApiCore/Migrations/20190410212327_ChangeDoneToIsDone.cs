using Microsoft.EntityFrameworkCore.Migrations;

namespace ApiCore.Migrations
{
    public partial class ChangeDoneToIsDone : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Done",
                table: "Projects");

            migrationBuilder.AddColumn<bool>(
                name: "IsDone",
                table: "Projects",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDone",
                table: "Projects");

            migrationBuilder.AddColumn<bool>(
                name: "Done",
                table: "Projects",
                nullable: false,
                defaultValue: true);
        }
    }
}
