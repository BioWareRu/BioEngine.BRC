using System;
using System.Collections.Generic;
using BioEngine.BRC.Core.Entities;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BioEngine.BRC.Admin.Migrations
{
    public partial class NullabilityFixes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "TwitterPublishRecord",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid[]>(
                name: "SiteIds",
                table: "TwitterPublishRecord",
                nullable: false,
                oldClrType: typeof(Guid[]),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PublicUri",
                table: "StorageItems",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Path",
                table: "StorageItems",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FilePath",
                table: "StorageItems",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FileName",
                table: "StorageItems",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid[]>(
                name: "SiteIds",
                table: "Sections",
                nullable: false,
                oldClrType: typeof(Guid[]),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "EntityType",
                table: "Properties",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid[]>(
                name: "SiteIds",
                table: "Menus",
                nullable: false,
                oldClrType: typeof(Guid[]),
                oldNullable: true);

            migrationBuilder.AlterColumn<List<MenuItem>>(
                name: "Items",
                table: "Menus",
                type: "jsonb",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "jsonb",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "IPBPublishRecord",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid[]>(
                name: "SiteIds",
                table: "IPBPublishRecord",
                nullable: false,
                oldClrType: typeof(Guid[]),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "FacebookPublishRecord",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid[]>(
                name: "SiteIds",
                table: "FacebookPublishRecord",
                nullable: false,
                oldClrType: typeof(Guid[]),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Data",
                table: "ContentVersions",
                type: "jsonb",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "jsonb",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ChangeAuthorId",
                table: "ContentVersions",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid[]>(
                name: "TagIds",
                table: "Content",
                nullable: false,
                oldClrType: typeof(Guid[]),
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid[]>(
                name: "SiteIds",
                table: "Content",
                nullable: false,
                oldClrType: typeof(Guid[]),
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid[]>(
                name: "SectionIds",
                table: "Content",
                nullable: false,
                oldClrType: typeof(Guid[]),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Ads",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid[]>(
                name: "SiteIds",
                table: "Ads",
                nullable: false,
                oldClrType: typeof(Guid[]),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "TwitterPublishRecord",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<Guid[]>(
                name: "SiteIds",
                table: "TwitterPublishRecord",
                nullable: true,
                oldClrType: typeof(Guid[]));

            migrationBuilder.AlterColumn<string>(
                name: "PublicUri",
                table: "StorageItems",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Path",
                table: "StorageItems",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "FilePath",
                table: "StorageItems",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "FileName",
                table: "StorageItems",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<Guid[]>(
                name: "SiteIds",
                table: "Sections",
                nullable: true,
                oldClrType: typeof(Guid[]));

            migrationBuilder.AlterColumn<string>(
                name: "EntityType",
                table: "Properties",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<Guid[]>(
                name: "SiteIds",
                table: "Menus",
                nullable: true,
                oldClrType: typeof(Guid[]));

            migrationBuilder.AlterColumn<string>(
                name: "Items",
                table: "Menus",
                type: "jsonb",
                nullable: true,
                oldClrType: typeof(List<MenuItem>),
                oldType: "jsonb");

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "IPBPublishRecord",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<Guid[]>(
                name: "SiteIds",
                table: "IPBPublishRecord",
                nullable: true,
                oldClrType: typeof(Guid[]));

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "FacebookPublishRecord",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<Guid[]>(
                name: "SiteIds",
                table: "FacebookPublishRecord",
                nullable: true,
                oldClrType: typeof(Guid[]));

            migrationBuilder.AlterColumn<string>(
                name: "Data",
                table: "ContentVersions",
                type: "jsonb",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "jsonb");

            migrationBuilder.AlterColumn<string>(
                name: "ChangeAuthorId",
                table: "ContentVersions",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<Guid[]>(
                name: "TagIds",
                table: "Content",
                nullable: true,
                oldClrType: typeof(Guid[]));

            migrationBuilder.AlterColumn<Guid[]>(
                name: "SiteIds",
                table: "Content",
                nullable: true,
                oldClrType: typeof(Guid[]));

            migrationBuilder.AlterColumn<Guid[]>(
                name: "SectionIds",
                table: "Content",
                nullable: true,
                oldClrType: typeof(Guid[]));

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Ads",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<Guid[]>(
                name: "SiteIds",
                table: "Ads",
                nullable: true,
                oldClrType: typeof(Guid[]));

            migrationBuilder.CreateIndex(
                name: "IX_ContentBlocks_ContentId",
                table: "ContentBlocks",
                column: "ContentId");

            migrationBuilder.AddForeignKey(
                name: "FK_ContentBlocks_Content_ContentId",
                table: "ContentBlocks",
                column: "ContentId",
                principalTable: "Content",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ContentBlocks_Sections_ContentId",
                table: "ContentBlocks",
                column: "ContentId",
                principalTable: "Sections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ContentBlocks_Ads_ContentId",
                table: "ContentBlocks",
                column: "ContentId",
                principalTable: "Ads",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
