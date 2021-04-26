using Microsoft.EntityFrameworkCore.Migrations;

namespace GameAPI.Migrations
{
    public partial class ChangeNameOfFieldProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "isHit",
                table: "Fields",
                newName: "IsHit");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsHit",
                table: "Fields",
                newName: "isHit");
        }
    }
}
