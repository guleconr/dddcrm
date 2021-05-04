using Microsoft.EntityFrameworkCore.Migrations;

namespace TBBProject.Core.Data.Migrations
{
    public partial class Authorekleme07042020 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "UserId",
                table: "NewsLang",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "UserId",
                table: "AnnouncementLang",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_NewsLang_UserId",
                table: "NewsLang",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AnnouncementLang_UserId",
                table: "AnnouncementLang",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AnnouncementLang_Users_UserId",
                table: "AnnouncementLang",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NewsLang_Users_UserId",
                table: "NewsLang",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AnnouncementLang_Users_UserId",
                table: "AnnouncementLang");

            migrationBuilder.DropForeignKey(
                name: "FK_NewsLang_Users_UserId",
                table: "NewsLang");

            migrationBuilder.DropIndex(
                name: "IX_NewsLang_UserId",
                table: "NewsLang");

            migrationBuilder.DropIndex(
                name: "IX_AnnouncementLang_UserId",
                table: "AnnouncementLang");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "NewsLang");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "AnnouncementLang");
        }
    }
}
