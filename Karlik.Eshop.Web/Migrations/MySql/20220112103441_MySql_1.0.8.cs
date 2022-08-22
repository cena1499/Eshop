using Microsoft.EntityFrameworkCore.Migrations;

namespace Karlik.Eshop.Web.Migrations.MySql
{
    public partial class MySql_108 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageAltCZ",
                table: "CarouselItem",
                type: "varchar(50)",
                maxLength: 50,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageAltCZ",
                table: "CarouselItem");
        }
    }
}
