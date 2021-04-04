using Microsoft.EntityFrameworkCore.Migrations;

namespace TBBProject.Core.Data.Migrations
{
    public partial class GuncelDurumSorgulamaV2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NewsLang_New_NewsId",
                table: "NewsLang");

            migrationBuilder.DropPrimaryKey(
                name: "PK_New",
                table: "New");

            migrationBuilder.RenameTable(
                name: "New",
                newName: "News");

            migrationBuilder.AddPrimaryKey(
                name: "PK_News",
                table: "News",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_NewsLang_News_NewsId",
                table: "NewsLang",
                column: "NewsId",
                principalTable: "News",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NewsLang_News_NewsId",
                table: "NewsLang");

            migrationBuilder.DropPrimaryKey(
                name: "PK_News",
                table: "News");

            migrationBuilder.RenameTable(
                name: "News",
                newName: "New");

            migrationBuilder.AddPrimaryKey(
                name: "PK_New",
                table: "New",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_NewsLang_New_NewsId",
                table: "NewsLang",
                column: "NewsId",
                principalTable: "New",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
