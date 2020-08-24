using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SaleAnnouncements.DAL.Migrations
{
    public partial class v_02 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDate",
                table: "OffersStatusesMaps",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "OffersStatusesMaps");
        }
    }
}
