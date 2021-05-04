using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TBBProject.Core.Data.Migrations
{
    public partial class LegislationAnnouncementEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LegislationAnnouncement",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReleaseDate = table.Column<DateTime>(nullable: false),
                    IsRelease = table.Column<int>(nullable: false),
                    ApprovalStatus = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LegislationAnnouncement", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LegislationAnnouncementLang",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: true),
                    Content = table.Column<string>(nullable: true),
                    Image = table.Column<byte[]>(nullable: true),
                    LanguageId = table.Column<long>(nullable: false),
                    LegislationAnnouncementId = table.Column<long>(nullable: false),
                    UserId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LegislationAnnouncementLang", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LegislationAnnouncementLang_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LegislationAnnouncementLang_LegislationAnnouncement_LegislationAnnouncementId",
                        column: x => x.LegislationAnnouncementId,
                        principalTable: "LegislationAnnouncement",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LegislationAnnouncementLang_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LegislationAnnouncementLang_LanguageId",
                table: "LegislationAnnouncementLang",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_LegislationAnnouncementLang_LegislationAnnouncementId",
                table: "LegislationAnnouncementLang",
                column: "LegislationAnnouncementId");

            migrationBuilder.CreateIndex(
                name: "IX_LegislationAnnouncementLang_UserId",
                table: "LegislationAnnouncementLang",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LegislationAnnouncementLang");

            migrationBuilder.DropTable(
                name: "LegislationAnnouncement");
        }
    }
}
