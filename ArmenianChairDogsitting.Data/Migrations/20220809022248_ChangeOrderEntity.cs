using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ArmenianChairDogsitting.Data.Migrations
{
    public partial class ChangeOrderEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "OrderOverexpose_DayQuantity",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "OrderSittingForDay_WalkQuantity",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "OrderWalk_WalkQuantity",
                table: "Order");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                table: "Order",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateUpdated",
                table: "Order",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "District",
                table: "Order",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Order",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "WorkDate",
                table: "Order",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "DayQuantity",
                table: "Order",
                type: "nvarchar",
                nullable: true,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "WalkQuantity",
                table: "Order",
                type: "nvarchar",
                nullable: true,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "WalkPerDayQuantity",
                table: "Order",
                type: "nvarchar",
                nullable: true,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "HourQuantity",
                table: "Order",
                type: "nvarchar",
                nullable: true,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "VisitQuantity",
                table: "Order",
                type: "nvarchar",
                nullable: true,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "DateUpdated",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "District",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "WorkDate",
                table: "Order");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Order",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "OrderOverexpose_DayQuantity",
                table: "Order",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrderSittingForDay_WalkQuantity",
                table: "Order",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrderWalk_WalkQuantity",
                table: "Order",
                type: "int",
                nullable: true);
        }
    }
}
