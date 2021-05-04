using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TBBProject.Core.Data.Migrations
{
    public partial class annfileandlink : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "LegislationAnnouncementLang");

            migrationBuilder.AddColumn<byte[]>(
                name: "File",
                table: "LegislationAnnouncementLang",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FileName",
                table: "LegislationAnnouncementLang",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Link",
                table: "LegislationAnnouncementLang",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "File",
                table: "LegislationAnnouncementLang");

            migrationBuilder.DropColumn(
                name: "FileName",
                table: "LegislationAnnouncementLang");

            migrationBuilder.DropColumn(
                name: "Link",
                table: "LegislationAnnouncementLang");

            migrationBuilder.AddColumn<byte[]>(
                name: "Image",
                table: "LegislationAnnouncementLang",
                type: "varbinary(max)",
                nullable: true);
        }
    }
}
