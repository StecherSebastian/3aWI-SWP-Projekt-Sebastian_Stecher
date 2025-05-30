using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Projekt.Database.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Schools",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schools", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Classrooms",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Seats = table.Column<int>(type: "INTEGER", nullable: false),
                    Cynap = table.Column<bool>(type: "INTEGER", nullable: false),
                    SchoolID = table.Column<int>(type: "INTEGER", nullable: true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Size = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Classrooms", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Classrooms_Schools_SchoolID",
                        column: x => x.SchoolID,
                        principalTable: "Schools",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Schoolclass = table.Column<int>(type: "INTEGER", nullable: false),
                    Track = table.Column<int>(type: "INTEGER", nullable: false),
                    ClassroomID = table.Column<int>(type: "INTEGER", nullable: true),
                    SchoolID = table.Column<int>(type: "INTEGER", nullable: true),
                    FirstName = table.Column<string>(type: "TEXT", nullable: false),
                    LastName = table.Column<string>(type: "TEXT", nullable: false),
                    Birthdate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Gender = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Students_Classrooms_ClassroomID",
                        column: x => x.ClassroomID,
                        principalTable: "Classrooms",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Students_Schools_SchoolID",
                        column: x => x.SchoolID,
                        principalTable: "Schools",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Classrooms_SchoolID",
                table: "Classrooms",
                column: "SchoolID");

            migrationBuilder.CreateIndex(
                name: "IX_Students_ClassroomID",
                table: "Students",
                column: "ClassroomID");

            migrationBuilder.CreateIndex(
                name: "IX_Students_SchoolID",
                table: "Students",
                column: "SchoolID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "Classrooms");

            migrationBuilder.DropTable(
                name: "Schools");
        }
    }
}
