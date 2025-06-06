using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Projekt.Database.Migrations
{
    /// <inheritdoc />
    public partial class RenamedPersonBirthDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Birthdate",
                table: "Students",
                newName: "BirthDate");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BirthDate",
                table: "Students",
                newName: "Birthdate");
        }
    }
}
