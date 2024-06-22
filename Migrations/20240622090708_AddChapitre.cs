using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LearnHubBO.Migrations
{
    /// <inheritdoc />
    public partial class AddChapitre : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Chapitres",
                columns: table => new
                {
                    IdChapitre = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TitreChapitre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ordre = table.Column<int>(type: "int", nullable: false),
                    Contenu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdCours = table.Column<int>(type: "int", nullable: false),
                    DateCreationChapitre = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModificationChapitre = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chapitres", x => x.IdChapitre);
                    table.ForeignKey(
                        name: "FK_Chapitres_Courses_IdCours",
                        column: x => x.IdCours,
                        principalTable: "Courses",
                        principalColumn: "IdCours",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Chapitres_IdCours",
                table: "Chapitres",
                column: "IdCours");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Chapitres");
        }
    }
}
