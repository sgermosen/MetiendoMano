using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Persistence.DatabaseContext.Migrations
{
    public partial class AddContraints : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Url",
                table: "Photos",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Photos",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Photos",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "Photos",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Photos",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "Photos",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Photos",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "Photos",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "LikesPerPhoto",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "LikesPerPhoto",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "LikesPerPhoto",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "LikesPerPhoto",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "LikesPerPhoto",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "LikesPerPhoto",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "LikesPerPhoto",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Comment",
                table: "CommentsPerPhoto",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "CommentsPerPhoto",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "CommentsPerPhoto",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "CommentsPerPhoto",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "CommentsPerPhoto",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "CommentsPerPhoto",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "CommentsPerPhoto",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "CommentsPerPhoto",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "SeoUrl",
                table: "AspNetUsers",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Image",
                table: "AspNetUsers",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AboutUs",
                table: "AspNetUsers",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Photos_CreatedBy",
                table: "Photos",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Photos_DeletedBy",
                table: "Photos",
                column: "DeletedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Photos_UpdatedBy",
                table: "Photos",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_LikesPerPhoto_CreatedBy",
                table: "LikesPerPhoto",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_LikesPerPhoto_DeletedBy",
                table: "LikesPerPhoto",
                column: "DeletedBy");

            migrationBuilder.CreateIndex(
                name: "IX_LikesPerPhoto_UpdatedBy",
                table: "LikesPerPhoto",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_CommentsPerPhoto_CreatedBy",
                table: "CommentsPerPhoto",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_CommentsPerPhoto_DeletedBy",
                table: "CommentsPerPhoto",
                column: "DeletedBy");

            migrationBuilder.CreateIndex(
                name: "IX_CommentsPerPhoto_UpdatedBy",
                table: "CommentsPerPhoto",
                column: "UpdatedBy");

            migrationBuilder.AddForeignKey(
                name: "FK_CommentsPerPhoto_AspNetUsers_CreatedBy",
                table: "CommentsPerPhoto",
                column: "CreatedBy",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CommentsPerPhoto_AspNetUsers_DeletedBy",
                table: "CommentsPerPhoto",
                column: "DeletedBy",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CommentsPerPhoto_AspNetUsers_UpdatedBy",
                table: "CommentsPerPhoto",
                column: "UpdatedBy",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LikesPerPhoto_AspNetUsers_CreatedBy",
                table: "LikesPerPhoto",
                column: "CreatedBy",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LikesPerPhoto_AspNetUsers_DeletedBy",
                table: "LikesPerPhoto",
                column: "DeletedBy",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LikesPerPhoto_AspNetUsers_UpdatedBy",
                table: "LikesPerPhoto",
                column: "UpdatedBy",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Photos_AspNetUsers_CreatedBy",
                table: "Photos",
                column: "CreatedBy",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Photos_AspNetUsers_DeletedBy",
                table: "Photos",
                column: "DeletedBy",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Photos_AspNetUsers_UpdatedBy",
                table: "Photos",
                column: "UpdatedBy",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CommentsPerPhoto_AspNetUsers_CreatedBy",
                table: "CommentsPerPhoto");

            migrationBuilder.DropForeignKey(
                name: "FK_CommentsPerPhoto_AspNetUsers_DeletedBy",
                table: "CommentsPerPhoto");

            migrationBuilder.DropForeignKey(
                name: "FK_CommentsPerPhoto_AspNetUsers_UpdatedBy",
                table: "CommentsPerPhoto");

            migrationBuilder.DropForeignKey(
                name: "FK_LikesPerPhoto_AspNetUsers_CreatedBy",
                table: "LikesPerPhoto");

            migrationBuilder.DropForeignKey(
                name: "FK_LikesPerPhoto_AspNetUsers_DeletedBy",
                table: "LikesPerPhoto");

            migrationBuilder.DropForeignKey(
                name: "FK_LikesPerPhoto_AspNetUsers_UpdatedBy",
                table: "LikesPerPhoto");

            migrationBuilder.DropForeignKey(
                name: "FK_Photos_AspNetUsers_CreatedBy",
                table: "Photos");

            migrationBuilder.DropForeignKey(
                name: "FK_Photos_AspNetUsers_DeletedBy",
                table: "Photos");

            migrationBuilder.DropForeignKey(
                name: "FK_Photos_AspNetUsers_UpdatedBy",
                table: "Photos");

            migrationBuilder.DropIndex(
                name: "IX_Photos_CreatedBy",
                table: "Photos");

            migrationBuilder.DropIndex(
                name: "IX_Photos_DeletedBy",
                table: "Photos");

            migrationBuilder.DropIndex(
                name: "IX_Photos_UpdatedBy",
                table: "Photos");

            migrationBuilder.DropIndex(
                name: "IX_LikesPerPhoto_CreatedBy",
                table: "LikesPerPhoto");

            migrationBuilder.DropIndex(
                name: "IX_LikesPerPhoto_DeletedBy",
                table: "LikesPerPhoto");

            migrationBuilder.DropIndex(
                name: "IX_LikesPerPhoto_UpdatedBy",
                table: "LikesPerPhoto");

            migrationBuilder.DropIndex(
                name: "IX_CommentsPerPhoto_CreatedBy",
                table: "CommentsPerPhoto");

            migrationBuilder.DropIndex(
                name: "IX_CommentsPerPhoto_DeletedBy",
                table: "CommentsPerPhoto");

            migrationBuilder.DropIndex(
                name: "IX_CommentsPerPhoto_UpdatedBy",
                table: "CommentsPerPhoto");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Photos");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Photos");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "Photos");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Photos");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "Photos");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Photos");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Photos");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "LikesPerPhoto");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "LikesPerPhoto");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "LikesPerPhoto");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "LikesPerPhoto");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "LikesPerPhoto");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "LikesPerPhoto");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "LikesPerPhoto");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "CommentsPerPhoto");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "CommentsPerPhoto");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "CommentsPerPhoto");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "CommentsPerPhoto");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "CommentsPerPhoto");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "CommentsPerPhoto");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "CommentsPerPhoto");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<string>(
                name: "Url",
                table: "Photos",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Comment",
                table: "CommentsPerPhoto",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "SeoUrl",
                table: "AspNetUsers",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Image",
                table: "AspNetUsers",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AboutUs",
                table: "AspNetUsers",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 500,
                oldNullable: true);
        }
    }
}
