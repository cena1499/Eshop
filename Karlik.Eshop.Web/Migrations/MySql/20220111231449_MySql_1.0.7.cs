using Microsoft.EntityFrameworkCore.Migrations;

namespace Karlik.Eshop.Web.Migrations.MySql
{
    public partial class MySql_107 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LanguageItemID",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "DetailCZ",
                table: "ProductItem",
                type: "varchar(1024)",
                maxLength: 1024,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "ImageAltCZ",
                table: "ProductItem",
                type: "varchar(50)",
                maxLength: 50,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "ProductNameCZ",
                table: "ProductItem",
                type: "varchar(50)",
                maxLength: 50,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LanguageItemID",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "DetailCZ",
                table: "ProductItem");

            migrationBuilder.DropColumn(
                name: "ImageAltCZ",
                table: "ProductItem");

            migrationBuilder.DropColumn(
                name: "ProductNameCZ",
                table: "ProductItem");
        }
    }
}
