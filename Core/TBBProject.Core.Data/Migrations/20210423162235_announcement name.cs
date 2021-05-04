using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TBBProject.Core.Data.Migrations
{
    public partial class announcementname : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageName",
                table: "AnnouncementLang",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AnnouncementFile",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    File = table.Column<byte[]>(nullable: true),
                    AnnouncementLangId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnnouncementFile", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AnnouncementFile_AnnouncementLang_AnnouncementLangId",
                        column: x => x.AnnouncementLangId,
                        principalTable: "AnnouncementLang",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AnnouncementFile_AnnouncementLangId",
                table: "AnnouncementFile",
                column: "AnnouncementLangId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnnouncementFile");

            migrationBuilder.DropColumn(
                name: "ImageName",
                table: "AnnouncementLang");
        }
    }
}
