using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TBBProject.Core.Data.Migrations
{
    public partial class contentfiles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageName",
                table: "ContentLang",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ContentFile",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    File = table.Column<byte[]>(nullable: true),
                    ContentLangId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContentFile", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContentFile_ContentLang_ContentLangId",
                        column: x => x.ContentLangId,
                        principalTable: "ContentLang",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ContentGallery",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Image = table.Column<byte[]>(nullable: true),
                    ContentLangId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContentGallery", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContentGallery_ContentLang_ContentLangId",
                        column: x => x.ContentLangId,
                        principalTable: "ContentLang",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContentFile_ContentLangId",
                table: "ContentFile",
                column: "ContentLangId");

            migrationBuilder.CreateIndex(
                name: "IX_ContentGallery_ContentLangId",
                table: "ContentGallery",
                column: "ContentLangId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContentFile");

            migrationBuilder.DropTable(
                name: "ContentGallery");

            migrationBuilder.DropColumn(
                name: "ImageName",
                table: "ContentLang");
        }
    }
}
