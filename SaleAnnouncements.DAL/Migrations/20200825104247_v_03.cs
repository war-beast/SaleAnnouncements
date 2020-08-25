using Microsoft.EntityFrameworkCore.Migrations;

namespace SaleAnnouncements.DAL.Migrations
{
    public partial class v_03 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Offers",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Offers",
                type: "decimal(18, 2)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Offers");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Offers");
        }
    }
}
