using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GameAPI.Migrations
{
    public partial class AddOrientationForShipAndHitsListForBoard : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NearHits",
                table: "Boards",
                newName: "ShipNearFields");

            migrationBuilder.AddColumn<string>(
                name: "Orientation",
                table: "Ships",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<List<int>>(
                name: "HitsList",
                table: "Boards",
                type: "integer[]",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Orientation",
                table: "Ships");

            migrationBuilder.DropColumn(
                name: "HitsList",
                table: "Boards");

            migrationBuilder.RenameColumn(
                name: "ShipNearFields",
                table: "Boards",
                newName: "NearHits");
        }
    }
}
