using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BioEngine.BRC.Admin.Migrations
{
    public partial class UpdComments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AddColumn<Guid>(
                name: "ReplyTo",
                table: "IPBComment",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Text",
                table: "IPBComment",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Ads",
                nullable: true,
                oldClrType: typeof(string));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReplyTo",
                table: "IPBComment");

            migrationBuilder.DropColumn(
                name: "Text",
                table: "IPBComment");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Ads",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
