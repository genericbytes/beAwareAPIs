using Microsoft.EntityFrameworkCore.Migrations;

namespace beAware_services.Migrations
{
    public partial class UpdateNewsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CountryName",
                table: "News",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StateName",
                table: "News",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CountryName",
                table: "News");

            migrationBuilder.DropColumn(
                name: "StateName",
                table: "News");
        }
    }
}
