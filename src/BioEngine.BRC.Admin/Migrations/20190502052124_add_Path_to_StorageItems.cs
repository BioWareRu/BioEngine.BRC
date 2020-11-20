using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BioEngine.BRC.Admin.Migrations
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public partial class add_Path_to_StorageItems : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                "Path",
                "StorageItems",
                nullable: false,
                defaultValue: "");

            migrationBuilder.Sql(
                "UPDATE \"StorageItems\" SET \"Path\" = regexp_replace(\"FilePath\", '\\/[^\\/]+$', '')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                "Path",
                "StorageItems");
        }
    }
}
