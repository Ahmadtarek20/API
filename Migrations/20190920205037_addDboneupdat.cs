using Microsoft.EntityFrameworkCore.Migrations;

namespace AppApi.Migrations
{
    public partial class addDboneupdat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Photos");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Photos",
                newName: "PhotoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PhotoId",
                table: "Photos",
                newName: "Id");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Photos",
                nullable: true);
        }
    }
}
