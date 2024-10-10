using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Civitta.TechnicalTask.PublicHolidays.Migrations
{
    /// <inheritdoc />
    public partial class _20241010_1508_migrate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.CreateTable(
                name: "Holidays",
                columns: table => new
                {
                    HolidayId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DayOfWeek = table.Column<int>(type: "int", nullable: false),
                    HolidayType = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Holidays", x => x.HolidayId);
                });

            migrationBuilder.CreateTable(
                name: "HolidayNames",
                columns: table => new
                {
                    Lang = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    NameId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HolidayNames", x => new { x.Lang, x.Text });
                    table.ForeignKey(
                        name: "FK_HolidayNames_Holidays_NameId",
                        column: x => x.NameId,
                        principalTable: "Holidays",
                        principalColumn: "HolidayId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_HolidayNames_NameId",
                table: "HolidayNames",
                column: "NameId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Countries");

            migrationBuilder.DropTable(
                name: "HolidayNames");

            migrationBuilder.DropTable(
                name: "Holidays");
        }
    }
}
