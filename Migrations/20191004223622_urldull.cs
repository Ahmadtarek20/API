using Microsoft.EntityFrameworkCore.Migrations;

namespace AppApi.Migrations
{
    public partial class urldull : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Url",
                table: "Products");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "Products",
                nullable: true);
        }
    }
}
