using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BioEngine.BRC.Core.Db.Migrations
{
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public partial class add_post_versioning : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                "PostVersions",
                table => new
                {
                    Id = table.Column<Guid>(),
                    DateAdded = table.Column<DateTimeOffset>(),
                    DateUpdated = table.Column<DateTimeOffset>(),
                    IsPublished = table.Column<bool>(),
                    DatePublished = table.Column<DateTimeOffset>(nullable: true),
                    PostId = table.Column<Guid>(),
                    Data = table.Column<string>("jsonb", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostVersions", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "PostVersions");
        }
    }
}
