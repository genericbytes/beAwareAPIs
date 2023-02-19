using Microsoft.EntityFrameworkCore.Migrations;

namespace beAware_services.Migrations
{
    public partial class UpdateReportNewsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Reason",
                table: "ReportedNews",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Reason",
                table: "ReportedNews");
        }
    }
}
