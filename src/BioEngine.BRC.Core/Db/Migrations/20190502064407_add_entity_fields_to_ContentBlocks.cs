using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BioEngine.BRC.Core.Db.Migrations
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public partial class add_entity_fields_to_ContentBlocks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTimeOffset>(
                "DateAdded",
                "ContentBlocks",
                nullable: false,
                defaultValue: DateTimeOffset.UtcNow
            );

            migrationBuilder.AddColumn<DateTimeOffset>(
                "DatePublished",
                "ContentBlocks",
                nullable: true,
                defaultValue: DateTimeOffset.UtcNow);

            migrationBuilder.AddColumn<DateTimeOffset>(
                "DateUpdated",
                "ContentBlocks",
                nullable: false,
                defaultValue: DateTimeOffset.UtcNow);

            migrationBuilder.AddColumn<bool>(
                "IsPublished",
                "ContentBlocks",
                nullable: false,
                defaultValue: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                "DateAdded",
                "ContentBlocks");

            migrationBuilder.DropColumn(
                "DatePublished",
                "ContentBlocks");

            migrationBuilder.DropColumn(
                "DateUpdated",
                "ContentBlocks");

            migrationBuilder.DropColumn(
                "IsPublished",
                "ContentBlocks");
        }
    }
}
