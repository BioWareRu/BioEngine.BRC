using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BioEngine.BRC.Admin.Migrations
{
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public partial class make_url_in_post_unique : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                "IX_Posts_Url",
                "Posts");

            migrationBuilder.CreateIndex(
                "IX_Posts_Url",
                "Posts",
                "Url",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                "IX_Posts_Url",
                "Posts");

            migrationBuilder.CreateIndex(
                "IX_Posts_Url",
                "Posts",
                "Url");
        }
    }
}
