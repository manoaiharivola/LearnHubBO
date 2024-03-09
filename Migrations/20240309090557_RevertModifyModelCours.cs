using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LearnHubBO.Migrations
{
    /// <inheritdoc />
    public partial class RevertModifyModelCours : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Courses_IdCoursCategorie",
                table: "Courses",
                column: "IdCoursCategorie");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_IdFormateur",
                table: "Courses",
                column: "IdFormateur");

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

            migrationBuilder.DropIndex(
                name: "IX_Courses_IdCoursCategorie",
                table: "Courses");

            migrationBuilder.DropIndex(
                name: "IX_Courses_IdFormateur",
                table: "Courses");
        }
    }
}
