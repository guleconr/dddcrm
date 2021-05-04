using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TBBProject.Core.Data.Migrations
{
    public partial class DynamicMenuCreatorMigrationV2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DynamicMenuResult",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Result = table.Column<string>(nullable: true),
                    EnterDate = table.Column<DateTime>(nullable: false),
                    MyProperty = table.Column<long>(nullable: false),
                    DynamicMenuId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DynamicMenuResult", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DynamicMenuResult_DynamicMenus_DynamicMenuId",
                        column: x => x.DynamicMenuId,
                        principalTable: "DynamicMenus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DynamicMenuResult_DynamicMenuId",
                table: "DynamicMenuResult",
                column: "DynamicMenuId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DynamicMenuResult");
        }
    }
}
