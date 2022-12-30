using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestionFilm.Migrations
{
    /// <inheritdoc />
    public partial class Migration1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "categories",
                columns: table => new
                {
                    idcategory = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    libellecategorie = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_categories", x => x.idcategory);
                });

            migrationBuilder.CreateTable(
                name: "films",
                columns: table => new
                {
                    IdFilm = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nomfilm = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    realisateurfilm = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    dureefilm = table.Column<int>(type: "int", nullable: false),
                    anneesortie = table.Column<int>(type: "int", nullable: false),
                    categoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_films", x => x.IdFilm);
                    table.ForeignKey(
                        name: "FK_films_categories_categoryId",
                        column: x => x.categoryId,
                        principalTable: "categories",
                        principalColumn: "idcategory",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_films_categoryId",
                table: "films",
                column: "categoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "films");

            migrationBuilder.DropTable(
                name: "categories");
        }
    }
}
