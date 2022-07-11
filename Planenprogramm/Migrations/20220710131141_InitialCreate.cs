using Microsoft.EntityFrameworkCore.Migrations;

namespace Planenprogramm.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TarpTypes",
                columns: table => new
                {
                    TarpTypeId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TarpTypes", x => x.TarpTypeId);
                });

            migrationBuilder.CreateTable(
                name: "TarpCategories",
                columns: table => new
                {
                    TarpCategoryId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Length = table.Column<int>(type: "INTEGER", nullable: true),
                    Width = table.Column<int>(type: "INTEGER", nullable: true),
                    Additional = table.Column<int>(type: "INTEGER", nullable: true),
                    TarpTypeId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TarpCategories", x => x.TarpCategoryId);
                    table.ForeignKey(
                        name: "FK_TarpCategories_TarpTypes_TarpTypeId",
                        column: x => x.TarpTypeId,
                        principalTable: "TarpTypes",
                        principalColumn: "TarpTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tarps",
                columns: table => new
                {
                    TarpId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TarpTypeId = table.Column<int>(type: "INTEGER", nullable: false),
                    TarpCategoryId = table.Column<int>(type: "INTEGER", nullable: false),
                    Annotation = table.Column<string>(type: "TEXT", nullable: true),
                    Number = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tarps", x => x.TarpId);
                    table.ForeignKey(
                        name: "FK_Tarps_TarpCategories_TarpCategoryId",
                        column: x => x.TarpCategoryId,
                        principalTable: "TarpCategories",
                        principalColumn: "TarpCategoryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tarps_TarpTypes_TarpTypeId",
                        column: x => x.TarpTypeId,
                        principalTable: "TarpTypes",
                        principalColumn: "TarpTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TarpCategories_TarpTypeId",
                table: "TarpCategories",
                column: "TarpTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Tarps_TarpCategoryId",
                table: "Tarps",
                column: "TarpCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Tarps_TarpTypeId",
                table: "Tarps",
                column: "TarpTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tarps");

            migrationBuilder.DropTable(
                name: "TarpCategories");

            migrationBuilder.DropTable(
                name: "TarpTypes");
        }
    }
}
