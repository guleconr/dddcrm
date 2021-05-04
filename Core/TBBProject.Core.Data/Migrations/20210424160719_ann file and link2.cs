using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TBBProject.Core.Data.Migrations
{
    public partial class annfileandlink2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "File",
                table: "LegislationAnnouncementLang");

            migrationBuilder.DropColumn(
                name: "FileName",
                table: "LegislationAnnouncementLang");

            migrationBuilder.CreateTable(
                name: "LegislationAnnouncementFile",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    File = table.Column<byte[]>(nullable: true),
                    LegislationAnnouncementLangId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LegislationAnnouncementFile", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LegislationAnnouncementFile_LegislationAnnouncementLang_LegislationAnnouncementLangId",
                        column: x => x.LegislationAnnouncementLangId,
                        principalTable: "LegislationAnnouncementLang",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LegislationAnnouncementFile_LegislationAnnouncementLangId",
                table: "LegislationAnnouncementFile",
                column: "LegislationAnnouncementLangId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LegislationAnnouncementFile");

            migrationBuilder.AddColumn<byte[]>(
                name: "File",
                table: "LegislationAnnouncementLang",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FileName",
                table: "LegislationAnnouncementLang",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
