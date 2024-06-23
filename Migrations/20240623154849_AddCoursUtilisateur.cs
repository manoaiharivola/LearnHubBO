using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LearnHubBO.Migrations
{
    /// <inheritdoc />
    public partial class AddCoursUtilisateur : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CoursUtilisateur",
                columns: table => new
                {
                    IdCoursUtilisateur = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdCours = table.Column<int>(type: "int", nullable: false),
                    IdUtilisateur = table.Column<int>(type: "int", nullable: false),
                    DateCreationCoursUtilisateur = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoursUtilisateur", x => x.IdCoursUtilisateur);
                    table.ForeignKey(
                        name: "FK_CoursUtilisateur_Courses_IdCours",
                        column: x => x.IdCours,
                        principalTable: "Courses",
                        principalColumn: "IdCours",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CoursUtilisateur_Utilisateurs_IdUtilisateur",
                        column: x => x.IdUtilisateur,
                        principalTable: "Utilisateurs",
                        principalColumn: "IdUtilisateur",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CoursUtilisateur_IdCours",
                table: "CoursUtilisateur",
                column: "IdCours");

            migrationBuilder.CreateIndex(
                name: "IX_CoursUtilisateur_IdUtilisateur",
                table: "CoursUtilisateur",
                column: "IdUtilisateur");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CoursUtilisateur");
        }
    }
}
