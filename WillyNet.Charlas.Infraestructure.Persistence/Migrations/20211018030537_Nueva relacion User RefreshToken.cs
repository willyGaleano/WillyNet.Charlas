using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WillyNet.Charlas.Infraestructure.Persistence.Migrations
{
    public partial class NuevarelacionUserRefreshToken : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RefreshToken_AspNetUsers_UserAppId",
                table: "RefreshToken");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RefreshToken",
                table: "RefreshToken");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "RefreshToken");

            migrationBuilder.RenameColumn(
                name: "CreatedRe",
                table: "RefreshToken",
                newName: "CreatedToken");

            migrationBuilder.AlterColumn<string>(
                name: "UserAppId",
                table: "RefreshToken",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<Guid>(
                name: "RefreshTokenId",
                table: "RefreshToken",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "RefreshToken",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "RefreshToken",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModified",
                table: "RefreshToken",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "RefreshToken",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_RefreshToken",
                table: "RefreshToken",
                column: "RefreshTokenId");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshToken_UserAppId",
                table: "RefreshToken",
                column: "UserAppId");

            migrationBuilder.AddForeignKey(
                name: "FK_RefreshToken_AspNetUsers_UserAppId",
                table: "RefreshToken",
                column: "UserAppId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RefreshToken_AspNetUsers_UserAppId",
                table: "RefreshToken");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RefreshToken",
                table: "RefreshToken");

            migrationBuilder.DropIndex(
                name: "IX_RefreshToken_UserAppId",
                table: "RefreshToken");

            migrationBuilder.DropColumn(
                name: "RefreshTokenId",
                table: "RefreshToken");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "RefreshToken");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "RefreshToken");

            migrationBuilder.DropColumn(
                name: "LastModified",
                table: "RefreshToken");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "RefreshToken");

            migrationBuilder.RenameColumn(
                name: "CreatedToken",
                table: "RefreshToken",
                newName: "CreatedRe");

            migrationBuilder.AlterColumn<string>(
                name: "UserAppId",
                table: "RefreshToken",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "RefreshToken",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RefreshToken",
                table: "RefreshToken",
                columns: new[] { "UserAppId", "Id" });

            migrationBuilder.AddForeignKey(
                name: "FK_RefreshToken_AspNetUsers_UserAppId",
                table: "RefreshToken",
                column: "UserAppId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
