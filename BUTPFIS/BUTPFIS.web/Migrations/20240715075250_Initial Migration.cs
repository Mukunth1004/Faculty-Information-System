using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BUTPFIS.web.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FacultyInfos",
                columns: table => new
                {
                    FId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Designation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MobileNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PersonalInfo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Expertise = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Experience = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Education = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Honours = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Patents = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Publications = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FacultyInfos", x => x.FId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FacultyInfos");
        }
    }
}
