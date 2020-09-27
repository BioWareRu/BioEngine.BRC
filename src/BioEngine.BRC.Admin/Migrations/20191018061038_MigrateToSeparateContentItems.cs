using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BioEngine.BRC.Admin.Migrations
{
    public partial class MigrateToSeparateContentItems : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                "Posts",
                table => new
                {
                    Id = table.Column<Guid>(),
                    Title = table.Column<string>(),
                    Url = table.Column<string>(),
                    AuthorId = table.Column<string>(),
                    SiteIds = table.Column<Guid[]>(),
                    SectionIds = table.Column<Guid[]>(),
                    TagIds = table.Column<Guid[]>(),
                    IsPublished = table.Column<bool>(),
                    DateAdded = table.Column<DateTimeOffset>(),
                    DateUpdated = table.Column<DateTimeOffset>(),
                    DatePublished = table.Column<DateTimeOffset>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                "Pages",
                table => new
                {
                    Id = table.Column<Guid>(),
                    Title = table.Column<string>(),
                    Url = table.Column<string>(),
                    SiteIds = table.Column<Guid[]>(),
                    IsPublished = table.Column<bool>(),
                    DateAdded = table.Column<DateTimeOffset>(),
                    DateUpdated = table.Column<DateTimeOffset>(),
                    DatePublished = table.Column<DateTimeOffset>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pages", x => x.Id);
                });

            migrationBuilder.DropPrimaryKey(
                "PK_ContentItemTemplates",
                "ContentItemTemplates");
            migrationBuilder.RenameTable(
                "ContentItemTemplates",
                newName: "PostTemplates");
            migrationBuilder.AddPrimaryKey(
                "PK_PostTemplates",
                "PostTemplates",
                "Id");

            migrationBuilder.DropPrimaryKey(
                "PK_ContentVersions",
                "ContentVersions");
            migrationBuilder.RenameTable(
                "ContentVersions",
                newName: "PostVersions");
            migrationBuilder.AddPrimaryKey(
                "PK_PostVersions",
                "PostVersions",
                "Id");

            migrationBuilder.Sql(
                @"INSERT INTO ""Posts"" (""Id"",""DateAdded"",""DateUpdated"",""AuthorId"",""Url"",""SiteIds"",""Title"",""IsPublished"",""DatePublished"",""TagIds"",""SectionIds"") SELECT ""Id"",""DateAdded"",""DateUpdated"",""AuthorId"",""Url"",""SiteIds"",""Title"",""IsPublished"",""DatePublished"",""TagIds"",""SectionIds"" FROM ""Content"" WHERE ""Type"" = 'postcontentitem'");
            migrationBuilder.Sql(
                @"INSERT INTO ""Pages"" (""Id"",""DateAdded"",""DateUpdated"",""Url"",""SiteIds"",""Title"",""IsPublished"",""DatePublished"") SELECT ""Id"",""DateAdded"",""DateUpdated"",""Url"",""SiteIds"",""Title"",""IsPublished"",""DatePublished"" FROM ""Content"" WHERE ""Type"" = 'pagecontentitem'");

            migrationBuilder.DropForeignKey(
                "FK_IPBComment_Content_ContentId",
                "IPBComment");

            migrationBuilder.DropIndex(
                "IX_IPBComment_ContentId",
                "IPBComment");

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

            migrationBuilder.CreateIndex(
                "IX_Pages_IsPublished",
                "Pages",
                "IsPublished");

            migrationBuilder.CreateIndex(
                "IX_Pages_SiteIds",
                "Pages",
                "SiteIds");

            migrationBuilder.CreateIndex(
                "IX_Pages_Url",
                "Pages",
                "Url",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "Posts");

            migrationBuilder.DropTable(
                "Pages");

            migrationBuilder.CreateIndex(
                "IX_IPBComment_ContentId",
                "IPBComment",
                "ContentId");

            migrationBuilder.AddForeignKey(
                "FK_IPBComment_Content_ContentId",
                "IPBComment",
                "ContentId",
                "Content",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.DropPrimaryKey(
                "PK_PostTemplates",
                "PostTemplates");
            migrationBuilder.RenameTable(
                "PostTemplates",
                newName: "ContentItemTemplates");
            migrationBuilder.AddPrimaryKey(
                "PK_ContentItemTemplates",
                "ContentItemTemplates", "Id");

            migrationBuilder.DropPrimaryKey(
                "PK_PostVersions",
                "PostVersions");
            migrationBuilder.RenameTable(
                "PostVersions",
                newName: "ContentVersions");
            migrationBuilder.AddPrimaryKey(
                "PK_ContentVersions",
                "ContentVersions",
                "Id");
        }
    }
}
