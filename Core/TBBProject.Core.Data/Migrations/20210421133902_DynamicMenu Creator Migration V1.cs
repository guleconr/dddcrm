using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TBBProject.Core.Data.Migrations
{
    public partial class DynamicMenuCreatorMigrationV1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DynamicMenus",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<long>(nullable: false),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DynamicMenus", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DynamicMenus_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DynamicMenuItem",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MenuType = table.Column<int>(nullable: false),
                    Values = table.Column<string>(nullable: true),
                    PropertyName = table.Column<string>(nullable: true),
                    DynamicMenuId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DynamicMenuItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DynamicMenuItem_DynamicMenus_DynamicMenuId",
                        column: x => x.DynamicMenuId,
                        principalTable: "DynamicMenus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DynamicMenuItem_DynamicMenuId",
                table: "DynamicMenuItem",
                column: "DynamicMenuId");

            migrationBuilder.CreateIndex(
                name: "IX_DynamicMenus_UserId",
                table: "DynamicMenus",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DynamicMenuItem");

            migrationBuilder.DropTable(
                name: "DynamicMenus");
        }
    }
}
