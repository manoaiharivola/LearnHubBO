using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LearnHubBO.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CoursCategories",
                columns: table => new
                {
                    IdCoursCategorie = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomCoursCategorie = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoursCategories", x => x.IdCoursCategorie);
                });

            migrationBuilder.CreateTable(
                name: "Formateurs",
                columns: table => new
                {
                    IdFormateur = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomFormateur = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Formateurs", x => x.IdFormateur);
                });

            migrationBuilder.CreateTable(
                name: "Cours",
                columns: table => new
                {
                    IdCours = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TitreCours = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdFormateur = table.Column<int>(type: "int", nullable: false),
                    IdCoursCategorie = table.Column<int>(type: "int", nullable: false),
                    DateCreationCours = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModificationCours = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cours", x => x.IdCours);
                    table.ForeignKey(
                        name: "FK_Cours_CoursCategories_IdCoursCategorie",
                        column: x => x.IdCoursCategorie,
                        principalTable: "CoursCategories",
                        principalColumn: "IdCoursCategorie",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Cours_Formateurs_IdFormateur",
                        column: x => x.IdFormateur,
                        principalTable: "Formateurs",
                        principalColumn: "IdFormateur",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cours_IdCoursCategorie",
                table: "Cours",
                column: "IdCoursCategorie");

            migrationBuilder.CreateIndex(
                name: "IX_Cours_IdFormateur",
                table: "Cours",
                column: "IdFormateur");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cours");

            migrationBuilder.DropTable(
                name: "CoursCategories");

            migrationBuilder.DropTable(
                name: "Formateurs");
        }
    }
}
