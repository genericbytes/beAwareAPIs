using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace beAware_services.Migrations
{
    public partial class modifyUsertable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "TillBlocked",
                table: "Users",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TillBlocked",
                table: "Users");
        }
    }
}
