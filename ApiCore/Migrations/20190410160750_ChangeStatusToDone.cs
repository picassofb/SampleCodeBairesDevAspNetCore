using Microsoft.EntityFrameworkCore.Migrations;

namespace ApiCore.Migrations
{
    public partial class ChangeStatusToDone : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Projects",
                newName: "Done");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Done",
                table: "Projects",
                newName: "Status");
        }
    }
}
