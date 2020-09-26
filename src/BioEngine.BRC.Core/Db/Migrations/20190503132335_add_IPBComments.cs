using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BioEngine.BRC.Core.Db.Migrations
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public partial class add_IPBComments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "Comments");

            migrationBuilder.CreateTable(
                "IPBComment",
                table => new
                {
                    Id = table.Column<Guid>(),
                    DateAdded = table.Column<DateTimeOffset>(),
                    DateUpdated = table.Column<DateTimeOffset>(),
                    IsPublished = table.Column<bool>(),
                    DatePublished = table.Column<DateTimeOffset>(nullable: true),
                    ContentId = table.Column<Guid>(),
                    Type = table.Column<string>(),
                    AuthorId = table.Column<int>(),
                    SiteIds = table.Column<Guid[]>(),
                    PostId = table.Column<int>()
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IPBComment", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "IPBComment");

            migrationBuilder.CreateTable(
                "Comments",
                table => new
                {
                    Id = table.Column<Guid>(),
                    AuthorId = table.Column<int>(),
                    ContentId = table.Column<Guid>(),
                    DateAdded = table.Column<DateTimeOffset>(),
                    DatePublished = table.Column<DateTimeOffset>(nullable: true),
                    DateUpdated = table.Column<DateTimeOffset>(),
                    IsPublished = table.Column<bool>(),
                    SiteIds = table.Column<Guid[]>(),
                    Type = table.Column<string>()
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                });
        }
    }
}
