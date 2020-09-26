using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BioEngine.BRC.Core.Db.Migrations
{
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public partial class init_state_with_guid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                "Menus",
                table => new
                {
                    Id = table.Column<Guid>(),
                    DateAdded = table.Column<DateTimeOffset>(),
                    DateUpdated = table.Column<DateTimeOffset>(),
                    IsPublished = table.Column<bool>(),
                    DatePublished = table.Column<DateTimeOffset>(nullable: true),
                    SiteId = table.Column<Guid>(),
                    Title = table.Column<string>(nullable: true),
                    Items = table.Column<string>("jsonb", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Menus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                "Pages",
                table => new
                {
                    Id = table.Column<Guid>(),
                    DateAdded = table.Column<DateTimeOffset>(),
                    DateUpdated = table.Column<DateTimeOffset>(),
                    IsPublished = table.Column<bool>(),
                    DatePublished = table.Column<DateTimeOffset>(nullable: true),
                    SiteIds = table.Column<Guid[]>(nullable: true),
                    Title = table.Column<string>(),
                    Url = table.Column<string>(),
                    Text = table.Column<string>()
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                "Posts",
                table => new
                {
                    Id = table.Column<Guid>(),
                    DateAdded = table.Column<DateTimeOffset>(),
                    DateUpdated = table.Column<DateTimeOffset>(),
                    IsPublished = table.Column<bool>(),
                    DatePublished = table.Column<DateTimeOffset>(nullable: true),
                    SiteIds = table.Column<Guid[]>(nullable: true),
                    AuthorId = table.Column<int>(),
                    Title = table.Column<string>(),
                    Url = table.Column<string>(),
                    IsPinned = table.Column<bool>(),
                    SectionIds = table.Column<Guid[]>(nullable: true),
                    TagIds = table.Column<Guid[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                "Properties",
                table => new
                {
                    Id = table.Column<Guid>(),
                    DateAdded = table.Column<DateTimeOffset>(),
                    DateUpdated = table.Column<DateTimeOffset>(),
                    IsPublished = table.Column<bool>(),
                    DatePublished = table.Column<DateTimeOffset>(nullable: true),
                    Key = table.Column<string>(),
                    EntityType = table.Column<string>(nullable: true),
                    EntityId = table.Column<Guid>(),
                    SiteId = table.Column<Guid>(nullable: true),
                    Data = table.Column<string>("jsonb")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Properties", x => x.Id);
                });

            migrationBuilder.CreateTable(
                "Sections",
                table => new
                {
                    Id = table.Column<Guid>(),
                    DateAdded = table.Column<DateTimeOffset>(),
                    DateUpdated = table.Column<DateTimeOffset>(),
                    IsPublished = table.Column<bool>(),
                    DatePublished = table.Column<DateTimeOffset>(nullable: true),
                    SiteIds = table.Column<Guid[]>(nullable: true),
                    Type = table.Column<string>(),
                    ParentId = table.Column<Guid>(nullable: true),
                    Title = table.Column<string>(),
                    Url = table.Column<string>(),
                    Logo = table.Column<string>("jsonb"),
                    LogoSmall = table.Column<string>("jsonb"),
                    ShortDescription = table.Column<string>(),
                    Hashtag = table.Column<string>(),
                    Data = table.Column<string>("jsonb", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sections", x => x.Id);
                });

            migrationBuilder.CreateTable(
                "Sites",
                table => new
                {
                    Id = table.Column<Guid>(),
                    DateAdded = table.Column<DateTimeOffset>(),
                    DateUpdated = table.Column<DateTimeOffset>(),
                    IsPublished = table.Column<bool>(),
                    DatePublished = table.Column<DateTimeOffset>(nullable: true),
                    Title = table.Column<string>(),
                    Url = table.Column<string>()
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sites", x => x.Id);
                });

            migrationBuilder.CreateTable(
                "StorageItems",
                table => new
                {
                    Id = table.Column<Guid>(),
                    DateAdded = table.Column<DateTimeOffset>(),
                    DateUpdated = table.Column<DateTimeOffset>(),
                    IsPublished = table.Column<bool>(),
                    DatePublished = table.Column<DateTimeOffset>(nullable: true),
                    FileName = table.Column<string>(nullable: true),
                    FileSize = table.Column<long>(),
                    PublicUri = table.Column<string>(nullable: true),
                    FilePath = table.Column<string>(nullable: true),
                    Type = table.Column<int>(),
                    PictureInfo = table.Column<string>("jsonb", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StorageItems", x => x.Id);
                });

            migrationBuilder.CreateTable(
                "Tags",
                table => new
                {
                    Id = table.Column<Guid>(),
                    DateAdded = table.Column<DateTimeOffset>(),
                    DateUpdated = table.Column<DateTimeOffset>(),
                    IsPublished = table.Column<bool>(),
                    DatePublished = table.Column<DateTimeOffset>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.Id);
                });

            migrationBuilder.CreateTable(
                "PostBlocks",
                table => new
                {
                    Id = table.Column<Guid>(),
                    PostId = table.Column<Guid>(),
                    Type = table.Column<string>(),
                    Position = table.Column<int>(),
                    Data = table.Column<string>("jsonb", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostBlocks", x => x.Id);
                    table.ForeignKey(
                        "FK_PostBlocks_Posts_PostId",
                        x => x.PostId,
                        "Posts",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                "IX_PostBlocks_PostId",
                "PostBlocks",
                "PostId");

            migrationBuilder.CreateIndex(
                "IX_Posts_IsPublished",
                "Posts",
                "IsPublished");

            migrationBuilder.CreateIndex(
                "IX_Posts_SectionIds",
                "Posts",
                "SectionIds");

            migrationBuilder.CreateIndex(
                "IX_Posts_SiteIds",
                "Posts",
                "SiteIds");

            migrationBuilder.CreateIndex(
                "IX_Posts_TagIds",
                "Posts",
                "TagIds");

            migrationBuilder.CreateIndex(
                "IX_Posts_Url",
                "Posts",
                "Url");

            migrationBuilder.CreateIndex(
                "IX_Sections_IsPublished",
                "Sections",
                "IsPublished");

            migrationBuilder.CreateIndex(
                "IX_Sections_SiteIds",
                "Sections",
                "SiteIds");

            migrationBuilder.CreateIndex(
                "IX_Sections_Type",
                "Sections",
                "Type");

            migrationBuilder.CreateIndex(
                "IX_Sections_Url",
                "Sections",
                "Url");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "Menus");

            migrationBuilder.DropTable(
                "Pages");

            migrationBuilder.DropTable(
                "PostBlocks");

            migrationBuilder.DropTable(
                "Properties");

            migrationBuilder.DropTable(
                "Sections");

            migrationBuilder.DropTable(
                "Sites");

            migrationBuilder.DropTable(
                "StorageItems");

            migrationBuilder.DropTable(
                "Tags");

            migrationBuilder.DropTable(
                "Posts");
        }
    }
}
