using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BioEngine.BRC.Admin.Migrations
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public partial class add_IpbContentSettings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                "IPBContentSettings",
                table => new
                {
                    Id = table.Column<Guid>(),
                    DateAdded = table.Column<DateTimeOffset>(),
                    DateUpdated = table.Column<DateTimeOffset>(),
                    IsPublished = table.Column<bool>(),
                    DatePublished = table.Column<DateTimeOffset>(nullable: true),
                    ContentId = table.Column<Guid>(),
                    Type = table.Column<string>(),
                    TopicId = table.Column<int>(),
                    PostId = table.Column<int>()
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IPBContentSettings", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "IPBContentSettings");
        }
    }
}
