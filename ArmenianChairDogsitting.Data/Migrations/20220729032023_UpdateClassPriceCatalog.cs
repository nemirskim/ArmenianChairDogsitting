using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ArmenianChairDogsitting.Data.Migrations
{
    public partial class UpdateClassPriceCatalog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PriceCatalog_Service_ServiceId",
                table: "PriceCatalog");

            migrationBuilder.DropForeignKey(
                name: "FK_Sitter_Service_ServiceId",
                table: "Sitter");

            migrationBuilder.DropTable(
                name: "Service");

            migrationBuilder.DropIndex(
                name: "IX_Sitter_ServiceId",
                table: "Sitter");

            migrationBuilder.DropIndex(
                name: "IX_PriceCatalog_ServiceId",
                table: "PriceCatalog");

            migrationBuilder.DropColumn(
                name: "ServiceId",
                table: "Sitter");

            migrationBuilder.RenameColumn(
                name: "ServiceId",
                table: "PriceCatalog",
                newName: "Service");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Client",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "ClientId",
                table: "Animal",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Animal_ClientId",
                table: "Animal",
                column: "ClientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Animal_Client_ClientId",
                table: "Animal",
                column: "ClientId",
                principalTable: "Client",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Animal_Client_ClientId",
                table: "Animal");

            migrationBuilder.DropIndex(
                name: "IX_Animal_ClientId",
                table: "Animal");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Client");

            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "Animal");

            migrationBuilder.RenameColumn(
                name: "Service",
                table: "PriceCatalog",
                newName: "ServiceId");

            migrationBuilder.AddColumn<int>(
                name: "ServiceId",
                table: "Sitter",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Service",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Service", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Sitter_ServiceId",
                table: "Sitter",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_PriceCatalog_ServiceId",
                table: "PriceCatalog",
                column: "ServiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_PriceCatalog_Service_ServiceId",
                table: "PriceCatalog",
                column: "ServiceId",
                principalTable: "Service",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Sitter_Service_ServiceId",
                table: "Sitter",
                column: "ServiceId",
                principalTable: "Service",
                principalColumn: "Id");
        }
    }
}
