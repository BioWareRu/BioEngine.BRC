using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BioEngine.BRC.Core.Db.Migrations
{
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public partial class drop_description_from_sections_use_blocks_instead : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                "ShortDescription",
                "Sections");

            migrationBuilder.DropColumn(
                "Text",
                "Pages");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                "ShortDescription",
                "Sections",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                "Text",
                "Pages",
                nullable: false,
                defaultValue: "");
        }
    }
}
