using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BioEngine.BRC.Admin.Migrations
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public partial class rework_content_entities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "Pages");

            migrationBuilder.DropTable(
                "Posts");

            migrationBuilder.DropTable(
                "PostVersions");

            migrationBuilder.DropColumn(
                "DatePublished",
                "TwitterPublishRecord");

            migrationBuilder.DropColumn(
                "IsPublished",
                "TwitterPublishRecord");

            migrationBuilder.DropColumn(
                "DatePublished",
                "Tags");

            migrationBuilder.DropColumn(
                "IsPublished",
                "Tags");

            migrationBuilder.DropColumn(
                "DatePublished",
                "StorageItems");

            migrationBuilder.DropColumn(
                "IsPublished",
                "StorageItems");

            migrationBuilder.DropColumn(
                "DatePublished",
                "Sites");

            migrationBuilder.DropColumn(
                "IsPublished",
                "Sites");

            migrationBuilder.DropColumn(
                "Hashtag",
                "Sections");

            migrationBuilder.DropColumn(
                "Logo",
                "Sections");

            migrationBuilder.DropColumn(
                "LogoSmall",
                "Sections");

            migrationBuilder.DropColumn(
                "DatePublished",
                "Properties");

            migrationBuilder.DropColumn(
                "IsPublished",
                "Properties");

            migrationBuilder.DropColumn(
                "DatePublished",
                "Menus");

            migrationBuilder.DropColumn(
                "IsPublished",
                "Menus");

            migrationBuilder.DropColumn(
                "SiteId",
                "Menus");

            migrationBuilder.DropColumn(
                "DatePublished",
                "IPBPublishRecord");

            migrationBuilder.DropColumn(
                "IsPublished",
                "IPBPublishRecord");

            migrationBuilder.DropColumn(
                "DatePublished",
                "IPBComment");

            migrationBuilder.DropColumn(
                "IsPublished",
                "IPBComment");

            migrationBuilder.DropColumn(
                "Type",
                "IPBComment");

            migrationBuilder.DropColumn(
                "DatePublished",
                "FacebookPublishRecord");

            migrationBuilder.DropColumn(
                "IsPublished",
                "FacebookPublishRecord");

            migrationBuilder.DropColumn(
                "DatePublished",
                "ContentBlocks");

            migrationBuilder.DropColumn(
                "IsPublished",
                "ContentBlocks");

            migrationBuilder.DropColumn(
                "Picture",
                "Ads");

            migrationBuilder.AddColumn<Guid[]>(
                "SiteIds",
                "Menus",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                "AdId",
                "ContentBlocks",
                nullable: true);

            migrationBuilder.CreateTable(
                "Content",
                table => new
                {
                    Id = table.Column<Guid>(),
                    Title = table.Column<string>(),
                    Url = table.Column<string>(),
                    DateAdded = table.Column<DateTimeOffset>(),
                    DateUpdated = table.Column<DateTimeOffset>(),
                    Type = table.Column<string>(),
                    IsPublished = table.Column<bool>(),
                    DatePublished = table.Column<DateTimeOffset>(nullable: true),
                    SiteIds = table.Column<Guid[]>(nullable: true),
                    SectionIds = table.Column<string>("jsonb", nullable: true),
                    TagIds = table.Column<string>("jsonb", nullable: true),
                    AuthorId = table.Column<int>(),
                    Data = table.Column<string>("jsonb", nullable: true),
                    IsPinned = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Content", x => x.Id);
                });

            migrationBuilder.CreateTable(
                "ContentVersions",
                table => new
                {
                    Id = table.Column<Guid>(),
                    DateAdded = table.Column<DateTimeOffset>(),
                    DateUpdated = table.Column<DateTimeOffset>(),
                    ContentId = table.Column<Guid>(),
                    Data = table.Column<string>("jsonb", nullable: true),
                    ChangeAuthorId = table.Column<int>()
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContentVersions", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                "IX_IPBComment_ContentId",
                "IPBComment",
                "ContentId");

            migrationBuilder.CreateIndex(
                "IX_ContentBlocks_AdId",
                "ContentBlocks",
                "AdId");

            migrationBuilder.CreateIndex(
                "IX_Content_IsPublished",
                "Content",
                "IsPublished");

            migrationBuilder.CreateIndex(
                "IX_Content_SectionIds",
                "Content",
                "SectionIds");

            migrationBuilder.CreateIndex(
                "IX_Content_SiteIds",
                "Content",
                "SiteIds");

            migrationBuilder.CreateIndex(
                "IX_Content_TagIds",
                "Content",
                "TagIds");

            migrationBuilder.CreateIndex(
                "IX_Content_Url",
                "Content",
                "Url",
                unique: true);

            migrationBuilder.AddForeignKey(
                "FK_ContentBlocks_Ads_AdId",
                "ContentBlocks",
                "AdId",
                "Ads",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                "FK_IPBComment_Content_ContentId",
                "IPBComment",
                "ContentId",
                "Content",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                "FK_ContentBlocks_Ads_AdId",
                "ContentBlocks");

            migrationBuilder.DropForeignKey(
                "FK_IPBComment_Content_ContentId",
                "IPBComment");

            migrationBuilder.DropTable(
                "Content");

            migrationBuilder.DropTable(
                "ContentVersions");

            migrationBuilder.DropIndex(
                "IX_IPBComment_ContentId",
                "IPBComment");

            migrationBuilder.DropIndex(
                "IX_ContentBlocks_AdId",
                "ContentBlocks");

            migrationBuilder.DropColumn(
                "SiteIds",
                "Menus");

            migrationBuilder.DropColumn(
                "AdId",
                "ContentBlocks");

            migrationBuilder.AddColumn<DateTimeOffset>(
                "DatePublished",
                "TwitterPublishRecord",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                "IsPublished",
                "TwitterPublishRecord",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTimeOffset>(
                "DatePublished",
                "Tags",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                "IsPublished",
                "Tags",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTimeOffset>(
                "DatePublished",
                "StorageItems",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                "IsPublished",
                "StorageItems",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTimeOffset>(
                "DatePublished",
                "Sites",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                "IsPublished",
                "Sites",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                "Hashtag",
                "Sections",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                "Logo",
                "Sections",
                "jsonb",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                "LogoSmall",
                "Sections",
                "jsonb",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTimeOffset>(
                "DatePublished",
                "Properties",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                "IsPublished",
                "Properties",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTimeOffset>(
                "DatePublished",
                "Menus",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                "IsPublished",
                "Menus",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                "SiteId",
                "Menus",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTimeOffset>(
                "DatePublished",
                "IPBPublishRecord",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                "IsPublished",
                "IPBPublishRecord",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTimeOffset>(
                "DatePublished",
                "IPBComment",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                "IsPublished",
                "IPBComment",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                "Type",
                "IPBComment",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTimeOffset>(
                "DatePublished",
                "FacebookPublishRecord",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                "IsPublished",
                "FacebookPublishRecord",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTimeOffset>(
                "DatePublished",
                "ContentBlocks",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                "IsPublished",
                "ContentBlocks",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                "Picture",
                "Ads",
                "jsonb",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                "Pages",
                table => new
                {
                    Id = table.Column<Guid>(),
                    DateAdded = table.Column<DateTimeOffset>(),
                    DatePublished = table.Column<DateTimeOffset>(nullable: true),
                    DateUpdated = table.Column<DateTimeOffset>(),
                    IsPublished = table.Column<bool>(),
                    SiteIds = table.Column<Guid[]>(nullable: true),
                    Title = table.Column<string>(),
                    Url = table.Column<string>()
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
                    AuthorId = table.Column<int>(),
                    DateAdded = table.Column<DateTimeOffset>(),
                    DatePublished = table.Column<DateTimeOffset>(nullable: true),
                    DateUpdated = table.Column<DateTimeOffset>(),
                    IsPinned = table.Column<bool>(),
                    IsPublished = table.Column<bool>(),
                    SectionIds = table.Column<Guid[]>(nullable: true),
                    SiteIds = table.Column<Guid[]>(nullable: true),
                    TagIds = table.Column<Guid[]>(nullable: true),
                    Title = table.Column<string>(),
                    Url = table.Column<string>()
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                "PostVersions",
                table => new
                {
                    Id = table.Column<Guid>(),
                    ChangeAuthorId = table.Column<int>(),
                    Data = table.Column<string>("jsonb", nullable: true),
                    DateAdded = table.Column<DateTimeOffset>(),
                    DatePublished = table.Column<DateTimeOffset>(nullable: true),
                    DateUpdated = table.Column<DateTimeOffset>(),
                    IsPublished = table.Column<bool>(),
                    PostId = table.Column<Guid>()
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostVersions", x => x.Id);
                });

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
                "Url",
                unique: true);
        }
    }
}
