using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Civitta.TechnicalTask.PublicHolidays.Migrations
{
    /// <inheritdoc />
    public partial class _20241010_1736_migrate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "Holidays",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Country",
                table: "Holidays");
        }
    }
}
