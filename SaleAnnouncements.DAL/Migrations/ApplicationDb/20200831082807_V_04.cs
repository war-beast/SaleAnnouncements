using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SaleAnnouncements.DAL.Migrations.ApplicationDb
{
    public partial class V_04 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "CompanionId",
                table: "Messages",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "CompanionId",
                table: "Messages",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(Guid));
        }
    }
}
