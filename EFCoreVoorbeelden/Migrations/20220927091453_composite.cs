using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFCoreVoorbeelden.Migrations
{
    public partial class composite : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_BarBeer",
                table: "BarBeer");

            migrationBuilder.DropIndex(
                name: "IX_BarBeer_BeerId",
                table: "BarBeer");

            migrationBuilder.DropColumn(
                name: "BarBeerId",
                table: "BarBeer");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BarBeer",
                table: "BarBeer",
                columns: new[] { "BeerId", "BarId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_BarBeer",
                table: "BarBeer");

            migrationBuilder.AddColumn<int>(
                name: "BarBeerId",
                table: "BarBeer",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BarBeer",
                table: "BarBeer",
                column: "BarBeerId");

            migrationBuilder.CreateIndex(
                name: "IX_BarBeer_BeerId",
                table: "BarBeer",
                column: "BeerId");
        }
    }
}
