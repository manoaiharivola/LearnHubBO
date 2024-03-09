using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LearnHubBO.Migrations
{
    /// <inheritdoc />
    public partial class ModifyCourses : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cours_CoursCategories_IdCoursCategorie",
                table: "Cours");

            migrationBuilder.DropForeignKey(
                name: "FK_Cours_Formateurs_IdFormateur",
                table: "Cours");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cours",
                table: "Cours");

            migrationBuilder.RenameTable(
                name: "Cours",
                newName: "Courses");

            migrationBuilder.RenameIndex(
                name: "IX_Cours_IdFormateur",
                table: "Courses",
                newName: "IX_Courses_IdFormateur");

            migrationBuilder.RenameIndex(
                name: "IX_Cours_IdCoursCategorie",
                table: "Courses",
                newName: "IX_Courses_IdCoursCategorie");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Courses",
                table: "Courses",
                column: "IdCours");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_CoursCategories_IdCoursCategorie",
                table: "Courses",
                column: "IdCoursCategorie",
                principalTable: "CoursCategories",
                principalColumn: "IdCoursCategorie",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Formateurs_IdFormateur",
                table: "Courses",
                column: "IdFormateur",
                principalTable: "Formateurs",
                principalColumn: "IdFormateur",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_CoursCategories_IdCoursCategorie",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Formateurs_IdFormateur",
                table: "Courses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Courses",
                table: "Courses");

            migrationBuilder.RenameTable(
                name: "Courses",
                newName: "Cours");

            migrationBuilder.RenameIndex(
                name: "IX_Courses_IdFormateur",
                table: "Cours",
                newName: "IX_Cours_IdFormateur");

            migrationBuilder.RenameIndex(
                name: "IX_Courses_IdCoursCategorie",
                table: "Cours",
                newName: "IX_Cours_IdCoursCategorie");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cours",
                table: "Cours",
                column: "IdCours");

            migrationBuilder.AddForeignKey(
                name: "FK_Cours_CoursCategories_IdCoursCategorie",
                table: "Cours",
                column: "IdCoursCategorie",
                principalTable: "CoursCategories",
                principalColumn: "IdCoursCategorie",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Cours_Formateurs_IdFormateur",
                table: "Cours",
                column: "IdFormateur",
                principalTable: "Formateurs",
                principalColumn: "IdFormateur",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
