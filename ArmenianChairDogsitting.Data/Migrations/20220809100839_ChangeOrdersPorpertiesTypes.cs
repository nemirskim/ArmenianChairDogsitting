using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ArmenianChairDogsitting.Data.Migrations
{
    public partial class ChangeOrdersPorpertiesTypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DayQuantity",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "WalkQuantity",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "WalkPerDayQuantity",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "HourQuantity",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "VisitQuantity",
                table: "Order");

            migrationBuilder.AddColumn<string>(
                name: "DayQuantity",
                table: "Order",
                type: "int",
                nullable: true,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "WalkQuantity",
                table: "Order",
                type: "int",
                nullable: true,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "WalkPerDayQuantity",
                table: "Order",
                type: "int",
                nullable: true,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "HourQuantity",
                table: "Order",
                type: "int",
                nullable: true,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "VisitQuantity",
                table: "Order",
                type: "int",
                nullable: true,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
