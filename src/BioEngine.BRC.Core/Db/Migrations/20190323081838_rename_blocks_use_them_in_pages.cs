using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BioEngine.BRC.Core.Db.Migrations
{
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public partial class rename_blocks_use_them_in_pages : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                "FK_PostBlocks_Posts_PostId",
                "PostBlocks");

            migrationBuilder.DropPrimaryKey(
                "PK_PostBlocks",
                "PostBlocks");

            migrationBuilder.RenameTable(
                "PostBlocks",
                newName: "ContentBlocks");

            migrationBuilder.RenameColumn(
                "PostId",
                "ContentBlocks",
                "ContentId");

            migrationBuilder.RenameIndex(
                "IX_PostBlocks_PostId",
                table: "ContentBlocks",
                newName: "IX_ContentBlocks_ContentId");

            migrationBuilder.AddPrimaryKey(
                "PK_ContentBlocks",
                "ContentBlocks",
                "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                "PK_ContentBlocks",
                "ContentBlocks");

            migrationBuilder.RenameTable(
                "ContentBlocks",
                newName: "PostBlocks");

            migrationBuilder.RenameColumn(
                "ContentId",
                "PostBlocks",
                "PostId");

            migrationBuilder.RenameIndex(
                "IX_ContentBlocks_ContentId",
                table: "PostBlocks",
                newName: "IX_PostBlocks_PostId");

            migrationBuilder.AddPrimaryKey(
                "PK_PostBlocks",
                "PostBlocks",
                "Id");

            migrationBuilder.AddForeignKey(
                "FK_PostBlocks_Posts_PostId",
                "PostBlocks",
                "PostId",
                "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
