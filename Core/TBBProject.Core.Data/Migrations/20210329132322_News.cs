using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TBBProject.Core.Data.Migrations
{
    public partial class News : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AnnouncementType",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnnouncementType", x => x.Id);
                });


            migrationBuilder.CreateTable(
                name: "New",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReleaseDate = table.Column<DateTime>(nullable: false),
                    IsRelease = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_New", x => x.Id);
                });

           

            migrationBuilder.CreateTable(
                name: "Announcements",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReleaseDate = table.Column<DateTime>(nullable: false),
                    IsRelease = table.Column<bool>(nullable: false),
                    AnnouncementTypeId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Announcements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Announcements_AnnouncementType_AnnouncementTypeId",
                        column: x => x.AnnouncementTypeId,
                        principalTable: "AnnouncementType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

           

            migrationBuilder.CreateTable(
                name: "NewsLang",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: true),
                    HeadLine = table.Column<string>(nullable: true),
                    Content = table.Column<string>(nullable: true),
                    SliderImage = table.Column<byte[]>(nullable: true),
                    NewsImage = table.Column<byte[]>(nullable: true),
                    NewsId = table.Column<long>(nullable: false),
                    LanguageId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewsLang", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NewsLang_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NewsLang_New_NewsId",
                        column: x => x.NewsId,
                        principalTable: "New",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

           

            migrationBuilder.CreateTable(
                name: "AnnouncementLang",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: true),
                    Content = table.Column<string>(nullable: true),
                    Image = table.Column<byte[]>(nullable: true),
                    LanguageId = table.Column<long>(nullable: false),
                    AnnouncementId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnnouncementLang", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AnnouncementLang_Announcements_AnnouncementId",
                        column: x => x.AnnouncementId,
                        principalTable: "Announcements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AnnouncementLang_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NewsGallery",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Image = table.Column<byte[]>(nullable: true),
                    NewsLangId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewsGallery", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NewsGallery_NewsLang_NewsLangId",
                        column: x => x.NewsLangId,
                        principalTable: "NewsLang",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AnnouncementLang_AnnouncementId",
                table: "AnnouncementLang",
                column: "AnnouncementId");

            migrationBuilder.CreateIndex(
                name: "IX_AnnouncementLang_LanguageId",
                table: "AnnouncementLang",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_Announcements_AnnouncementTypeId",
                table: "Announcements",
                column: "AnnouncementTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_NewsGallery_NewsLangId",
                table: "NewsGallery",
                column: "NewsLangId");

            migrationBuilder.CreateIndex(
                name: "IX_NewsLang_LanguageId",
                table: "NewsLang",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_NewsLang_NewsId",
                table: "NewsLang",
                column: "NewsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnnouncementLang");

           

            migrationBuilder.DropTable(
                name: "NewsGallery");

           

          

            migrationBuilder.DropTable(
                name: "Announcements");

            migrationBuilder.DropTable(
                name: "NewsLang");


            migrationBuilder.DropTable(
                name: "AnnouncementType");
        }
    }
}
