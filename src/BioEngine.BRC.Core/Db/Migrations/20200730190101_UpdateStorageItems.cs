using Microsoft.EntityFrameworkCore.Migrations;

namespace BioEngine.BRC.Core.Db.Migrations
{
    public partial class UpdateStorageItems : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Hash",
                table: "StorageItems");

            migrationBuilder.DropColumn(
                name: "PublicUri",
                table: "StorageItems");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "StorageItems");

            migrationBuilder.AlterColumn<string>(
                name: "Path",
                table: "StorageItems",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "FilePath",
                table: "StorageItems",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "FileName",
                table: "StorageItems",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.CreateIndex(
                name: "IX_TwitterPublishRecord_SiteIds",
                table: "TwitterPublishRecord",
                column: "SiteIds");

            migrationBuilder.CreateIndex(
                name: "IX_FacebookPublishRecord_SiteIds",
                table: "FacebookPublishRecord",
                column: "SiteIds");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TwitterPublishRecord_SiteIds",
                table: "TwitterPublishRecord");

            migrationBuilder.DropIndex(
                name: "IX_FacebookPublishRecord_SiteIds",
                table: "FacebookPublishRecord");

            migrationBuilder.AlterColumn<string>(
                name: "Path",
                table: "StorageItems",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FilePath",
                table: "StorageItems",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FileName",
                table: "StorageItems",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Hash",
                table: "StorageItems",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PublicUri",
                table: "StorageItems",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "StorageItems",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
