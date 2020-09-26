using System;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BioEngine.BRC.Core.Db.Migrations
{
    [UsedImplicitly]
    public partial class AddContentItemTemplates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                "ContentItemTemplates",
                table => new
                {
                    Id = table.Column<Guid>(),
                    Title = table.Column<string>(),
                    DateAdded = table.Column<DateTimeOffset>(),
                    DateUpdated = table.Column<DateTimeOffset>(),
                    ContentType = table.Column<string>(),
                    SectionIds = table.Column<Guid[]>(),
                    TagIds = table.Column<Guid[]>(),
                    AuthorId = table.Column<int>(),
                    Data = table.Column<string>("jsonb")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContentItemTemplates", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "ContentItemTemplates");
        }
    }
}
