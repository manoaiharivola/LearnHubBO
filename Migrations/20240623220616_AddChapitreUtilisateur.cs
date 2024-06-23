using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LearnHubBO.Migrations
{
    /// <inheritdoc />
    public partial class AddChapitreUtilisateur : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ChapitreUtilisateur",
                columns: table => new
                {
                    IdChapitreUtilisateur = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdChapitre = table.Column<int>(type: "int", nullable: false),
                    IdUtilisateur = table.Column<int>(type: "int", nullable: false),
                    DateCreationChapitreUtilisateur = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChapitreUtilisateur", x => x.IdChapitreUtilisateur);
                    table.ForeignKey(
                        name: "FK_ChapitreUtilisateur_Chapitres_IdChapitre",
                        column: x => x.IdChapitre,
                        principalTable: "Chapitres",
                        principalColumn: "IdChapitre",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChapitreUtilisateur_Utilisateurs_IdUtilisateur",
                        column: x => x.IdUtilisateur,
                        principalTable: "Utilisateurs",
                        principalColumn: "IdUtilisateur",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChapitreUtilisateur_IdChapitre",
                table: "ChapitreUtilisateur",
                column: "IdChapitre");

            migrationBuilder.CreateIndex(
                name: "IX_ChapitreUtilisateur_IdUtilisateur",
                table: "ChapitreUtilisateur",
                column: "IdUtilisateur");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChapitreUtilisateur");
        }
    }
}
