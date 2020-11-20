using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BioEngine.BRC.Admin.Migrations
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public partial class fix_relations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                "FK_ContentBlocks_Ads_AdId",
                "ContentBlocks");

            migrationBuilder.DropIndex(
                "IX_ContentBlocks_AdId",
                "ContentBlocks");

            migrationBuilder.DropColumn(
                "AdId",
                "ContentBlocks");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                "AdId",
                "ContentBlocks",
                nullable: true);

            migrationBuilder.CreateIndex(
                "IX_ContentBlocks_AdId",
                "ContentBlocks",
                "AdId");

            migrationBuilder.AddForeignKey(
                "FK_ContentBlocks_Ads_AdId",
                "ContentBlocks",
                "AdId",
                "Ads",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
