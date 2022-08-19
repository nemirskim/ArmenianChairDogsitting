using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ArmenianChairDogsitting.Data.Migrations
{
    public partial class AddCLientDataReg : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Animal_Client_ClientId",
                table: "Animal");

            migrationBuilder.AddColumn<DateTime>(
                name: "RegestrationDate",
                table: "Sitter",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<int>(
                name: "ClientId",
                table: "Animal",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Age",
                table: "Animal",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Breed",
                table: "Animal",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Animal",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "RecommendationsForCare",
                table: "Animal",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Size",
                table: "Animal",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Animal_Client_ClientId",
                table: "Animal",
                column: "ClientId",
                principalTable: "Client",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Animal_Client_ClientId",
                table: "Animal");

            migrationBuilder.DropColumn(
                name: "RegestrationDate",
                table: "Sitter");

            migrationBuilder.DropColumn(
                name: "Age",
                table: "Animal");

            migrationBuilder.DropColumn(
                name: "Breed",
                table: "Animal");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Animal");

            migrationBuilder.DropColumn(
                name: "RecommendationsForCare",
                table: "Animal");

            migrationBuilder.DropColumn(
                name: "Size",
                table: "Animal");

            migrationBuilder.AlterColumn<int>(
                name: "ClientId",
                table: "Animal",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Animal_Client_ClientId",
                table: "Animal",
                column: "ClientId",
                principalTable: "Client",
                principalColumn: "Id");
        }
    }
}
