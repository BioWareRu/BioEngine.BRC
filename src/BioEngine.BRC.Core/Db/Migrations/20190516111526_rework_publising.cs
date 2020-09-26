using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BioEngine.BRC.Core.Db.Migrations
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public partial class rework_publising : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "IPBContentSettings");

            migrationBuilder.CreateTable(
                "FacebookPublishRecord",
                table => new
                {
                    Id = table.Column<Guid>(),
                    DateAdded = table.Column<DateTimeOffset>(),
                    DateUpdated = table.Column<DateTimeOffset>(),
                    IsPublished = table.Column<bool>(),
                    DatePublished = table.Column<DateTimeOffset>(nullable: true),
                    SiteId = table.Column<Guid>(),
                    ContentId = table.Column<Guid>(),
                    Type = table.Column<string>(nullable: true),
                    PostId = table.Column<string>()
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FacebookPublishRecord", x => x.Id);
                });

            migrationBuilder.CreateTable(
                "IPBPublishRecord",
                table => new
                {
                    Id = table.Column<Guid>(),
                    DateAdded = table.Column<DateTimeOffset>(),
                    DateUpdated = table.Column<DateTimeOffset>(),
                    IsPublished = table.Column<bool>(),
                    DatePublished = table.Column<DateTimeOffset>(nullable: true),
                    SiteId = table.Column<Guid>(),
                    ContentId = table.Column<Guid>(),
                    Type = table.Column<string>(nullable: true),
                    TopicId = table.Column<int>(),
                    PostId = table.Column<int>()
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IPBPublishRecord", x => x.Id);
                });

            migrationBuilder.CreateTable(
                "TwitterPublishRecord",
                table => new
                {
                    Id = table.Column<Guid>(),
                    DateAdded = table.Column<DateTimeOffset>(),
                    DateUpdated = table.Column<DateTimeOffset>(),
                    IsPublished = table.Column<bool>(),
                    DatePublished = table.Column<DateTimeOffset>(nullable: true),
                    SiteId = table.Column<Guid>(),
                    ContentId = table.Column<Guid>(),
                    Type = table.Column<string>(nullable: true),
                    TweetId = table.Column<long>()
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TwitterPublishRecord", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "FacebookPublishRecord");

            migrationBuilder.DropTable(
                "IPBPublishRecord");

            migrationBuilder.DropTable(
                "TwitterPublishRecord");

            migrationBuilder.CreateTable(
                "IPBContentSettings",
                table => new
                {
                    Id = table.Column<Guid>(),
                    ContentId = table.Column<Guid>(),
                    DateAdded = table.Column<DateTimeOffset>(),
                    DatePublished = table.Column<DateTimeOffset>(nullable: true),
                    DateUpdated = table.Column<DateTimeOffset>(),
                    IsPublished = table.Column<bool>(),
                    PostId = table.Column<int>(),
                    TopicId = table.Column<int>(),
                    Type = table.Column<string>()
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IPBContentSettings", x => x.Id);
                });
        }
    }
}
