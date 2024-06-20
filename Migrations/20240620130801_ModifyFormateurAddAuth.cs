using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LearnHubBO.Migrations
{
    /// <inheritdoc />
    public partial class ModifyFormateurAddAuth : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Formateurs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MotDePasseHash",
                table: "Formateurs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Formateurs");

            migrationBuilder.DropColumn(
                name: "MotDePasseHash",
                table: "Formateurs");
        }
    }
}
