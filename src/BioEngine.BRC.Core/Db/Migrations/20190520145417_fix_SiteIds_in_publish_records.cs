using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BioEngine.BRC.Core.Db.Migrations
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public partial class fix_SiteIds_in_publish_records : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                "SiteId",
                "TwitterPublishRecord");

            migrationBuilder.DropColumn(
                "SiteId",
                "IPBPublishRecord");

            migrationBuilder.DropColumn(
                "SiteId",
                "FacebookPublishRecord");

            migrationBuilder.AddColumn<Guid[]>(
                "SiteIds",
                "TwitterPublishRecord",
                nullable: true);

            migrationBuilder.AddColumn<Guid[]>(
                "SiteIds",
                "IPBPublishRecord",
                nullable: true);

            migrationBuilder.AddColumn<Guid[]>(
                "SiteIds",
                "FacebookPublishRecord",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                "SiteIds",
                "TwitterPublishRecord");

            migrationBuilder.DropColumn(
                "SiteIds",
                "IPBPublishRecord");

            migrationBuilder.DropColumn(
                "SiteIds",
                "FacebookPublishRecord");

            migrationBuilder.AddColumn<Guid>(
                "SiteId",
                "TwitterPublishRecord",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                "SiteId",
                "IPBPublishRecord",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                "SiteId",
                "FacebookPublishRecord",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }
    }
}
