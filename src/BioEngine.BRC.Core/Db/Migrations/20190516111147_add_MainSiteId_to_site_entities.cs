using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BioEngine.BRC.Core.Db.Migrations
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public partial class add_MainSiteId_to_site_entities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           
            migrationBuilder.AddColumn<Guid>(
                "MainSiteId",
                "Sections",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                "MainSiteId",
                "Posts",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                "MainSiteId",
                "Pages",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                "MainSiteId",
                "Ads",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

           
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                "MainSiteId",
                "Sections");

            migrationBuilder.DropColumn(
                "MainSiteId",
                "Posts");

            migrationBuilder.DropColumn(
                "MainSiteId",
                "Pages");

            migrationBuilder.DropColumn(
                "MainSiteId",
                "Ads");
        }
    }
}
