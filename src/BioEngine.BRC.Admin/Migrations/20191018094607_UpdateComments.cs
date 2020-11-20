using Microsoft.EntityFrameworkCore.Migrations;

namespace BioEngine.BRC.Admin.Migrations
{
    public partial class UpdateComments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ContentType",
                table: "IPBComment",
                nullable: true);

            migrationBuilder.Sql(@"UPDATE ""IPBComment"" SET ""ContentType""='postcontentitem'");

            migrationBuilder.AlterColumn<string>(
                name: "ContentType",
                table: "IPBComment");

            migrationBuilder.CreateIndex(
                name: "IX_Ads_SiteIds",
                table: "Ads",
                column: "SiteIds");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Ads_SiteIds",
                table: "Ads");

            migrationBuilder.DropColumn(
                name: "ContentType",
                table: "IPBComment");
        }
    }
}
