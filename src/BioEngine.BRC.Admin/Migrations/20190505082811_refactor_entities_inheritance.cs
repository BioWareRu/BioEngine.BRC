using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BioEngine.BRC.Admin.Migrations
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public partial class refactor_entities_inheritance : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                "Name",
                "Tags",
                "Title");

            migrationBuilder.AlterColumn<string>(
                "Title",
                "Menus",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                "Title",
                "Tags",
                "Name");

            migrationBuilder.AlterColumn<string>(
                "Title",
                "Menus",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}
