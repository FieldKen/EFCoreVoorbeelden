using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFCoreVoorbeelden.Migrations
{
    public partial class BarBeer2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BarBeer",
                columns: table => new
                {
                    BarBeerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BarId = table.Column<int>(type: "int", nullable: false),
                    BeerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BarBeer", x => x.BarBeerId);
                    table.ForeignKey(
                        name: "FK_BarBeer_Bar_BarId",
                        column: x => x.BarId,
                        principalTable: "Bar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BarBeer_Beer_BeerId",
                        column: x => x.BeerId,
                        principalTable: "Beer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BarBeer_BarId",
                table: "BarBeer",
                column: "BarId");

            migrationBuilder.CreateIndex(
                name: "IX_BarBeer_BeerId",
                table: "BarBeer",
                column: "BeerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BarBeer");
        }
    }
}
