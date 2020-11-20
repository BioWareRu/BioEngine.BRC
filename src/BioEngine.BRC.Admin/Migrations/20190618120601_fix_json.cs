using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BioEngine.BRC.Admin.Migrations
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public partial class fix_json : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                @"ALTER TABLE ""Content"" ALTER COLUMN ""TagIds"" TYPE uuid[] USING string_to_array(translate(""TagIds""::text, '[]\"" ', ''), ',')::uuid[];");
            migrationBuilder.Sql(
                @"ALTER TABLE ""Content"" ALTER COLUMN ""SectionIds"" TYPE uuid[] USING string_to_array(translate(""SectionIds""::text, '[]\"" ', ''), ',')::uuid[];");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                "ALTER TABLE \"Content\" ALTER COLUMN \"TagIds\" TYPE jsonb USING to_jsonb(\"TagIds\")");
            migrationBuilder.Sql(
                "ALTER TABLE \"Content\" ALTER COLUMN \"SectionIds\" TYPE jsonb USING to_jsonb(\"SectionIds\")");
        }
    }
}
