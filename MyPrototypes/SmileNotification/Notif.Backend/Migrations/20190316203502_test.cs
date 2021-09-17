using Microsoft.EntityFrameworkCore.Migrations;

namespace Notif.Backend.Migrations
{
    public partial class test : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reactions_AspNetUsers_ApplicationUserId",
                table: "Reactions");

            migrationBuilder.AlterColumn<string>(
                name: "ApplicationUserId",
                table: "Reactions",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddForeignKey(
                name: "FK_Reactions_AspNetUsers_ApplicationUserId",
                table: "Reactions",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reactions_AspNetUsers_ApplicationUserId",
                table: "Reactions");

            migrationBuilder.AlterColumn<string>(
                name: "ApplicationUserId",
                table: "Reactions",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Reactions_AspNetUsers_ApplicationUserId",
                table: "Reactions",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
