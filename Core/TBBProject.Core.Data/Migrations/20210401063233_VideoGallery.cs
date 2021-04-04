using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TBBProject.Core.Data.Migrations
{
    public partial class VideoGallery : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AcademicSchedule",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    Quota = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AcademicSchedule", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VideoGallery",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReleaseDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VideoGallery", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AcademicScheduleLang",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: true),
                    Url = table.Column<string>(nullable: true),
                    AcademicScheduleId = table.Column<long>(nullable: false),
                    LanguageId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AcademicScheduleLang", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AcademicScheduleLang_AcademicSchedule_AcademicScheduleId",
                        column: x => x.AcademicScheduleId,
                        principalTable: "AcademicSchedule",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AcademicScheduleLang_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VideoGalleryLang",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: true),
                    Url = table.Column<string>(nullable: true),
                    VideoGalleryId = table.Column<long>(nullable: false),
                    LanguageId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VideoGalleryLang", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VideoGalleryLang_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VideoGalleryLang_VideoGallery_VideoGalleryId",
                        column: x => x.VideoGalleryId,
                        principalTable: "VideoGallery",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AcademicScheduleLang_AcademicScheduleId",
                table: "AcademicScheduleLang",
                column: "AcademicScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_AcademicScheduleLang_LanguageId",
                table: "AcademicScheduleLang",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_VideoGalleryLang_LanguageId",
                table: "VideoGalleryLang",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_VideoGalleryLang_VideoGalleryId",
                table: "VideoGalleryLang",
                column: "VideoGalleryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AcademicScheduleLang");

            migrationBuilder.DropTable(
                name: "VideoGalleryLang");

            migrationBuilder.DropTable(
                name: "AcademicSchedule");

            migrationBuilder.DropTable(
                name: "VideoGallery");
        }
    }
}
