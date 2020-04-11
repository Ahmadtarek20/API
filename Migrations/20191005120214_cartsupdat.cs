using Microsoft.EntityFrameworkCore.Migrations;

namespace AppApi.Migrations
{
    public partial class cartsupdat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartList_Users_Usersid",
                table: "CartList");

            migrationBuilder.DropIndex(
                name: "IX_CartList_Usersid",
                table: "CartList");

            migrationBuilder.DropColumn(
                name: "Usersid",
                table: "CartList");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Usersid",
                table: "CartList",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CartList_Usersid",
                table: "CartList",
                column: "Usersid");

            migrationBuilder.AddForeignKey(
                name: "FK_CartList_Users_Usersid",
                table: "CartList",
                column: "Usersid",
                principalTable: "Users",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
