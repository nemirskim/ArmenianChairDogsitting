using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ArmenianChairDogsitting.Data.Migrations
{
    public partial class AddIsDeletedToOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Order",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Order");
        }
    }
}
