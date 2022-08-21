using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Planenprogramm.Migrations
{
    public partial class damage_codes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Damages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Code = table.Column<char>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Damages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TarpDamages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TarpId = table.Column<int>(type: "INTEGER", nullable: false),
                    DamageId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TarpDamages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TarpDamages_Damages_DamageId",
                        column: x => x.DamageId,
                        principalTable: "Damages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TarpDamages_Tarps_TarpId",
                        column: x => x.TarpId,
                        principalTable: "Tarps",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TarpDamages_DamageId",
                table: "TarpDamages",
                column: "DamageId");

            migrationBuilder.CreateIndex(
                name: "IX_TarpDamages_TarpId",
                table: "TarpDamages",
                column: "TarpId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TarpDamages");

            migrationBuilder.DropTable(
                name: "Damages");
        }
    }
}
