using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BUTPFIS.web.Migrations
{
    /// <inheritdoc />
    public partial class Thirdmigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CourseInfos",
                columns: table => new
                {
                    CourseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CourseName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseInfos", x => x.CourseId);
                });

            migrationBuilder.CreateTable(
                name: "FacultyInfos",
                columns: table => new
                {
                    FId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Designation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MobileNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FacultyImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PersonalInfo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Expertise = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Experience = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Education = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Honours = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Patents = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Publications = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Seminar = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FacultyInfos", x => x.FId);
                });

            migrationBuilder.CreateTable(
                name: "CourseInfoFacultyInfo",
                columns: table => new
                {
                    CourseInfosCourseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FacultyInfosFId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseInfoFacultyInfo", x => new { x.CourseInfosCourseId, x.FacultyInfosFId });
                    table.ForeignKey(
                        name: "FK_CourseInfoFacultyInfo_CourseInfos_CourseInfosCourseId",
                        column: x => x.CourseInfosCourseId,
                        principalTable: "CourseInfos",
                        principalColumn: "CourseId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourseInfoFacultyInfo_FacultyInfos_FacultyInfosFId",
                        column: x => x.FacultyInfosFId,
                        principalTable: "FacultyInfos",
                        principalColumn: "FId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CourseInfoFacultyInfo_FacultyInfosFId",
                table: "CourseInfoFacultyInfo",
                column: "FacultyInfosFId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CourseInfoFacultyInfo");

            migrationBuilder.DropTable(
                name: "CourseInfos");

            migrationBuilder.DropTable(
                name: "FacultyInfos");
        }
    }
}
