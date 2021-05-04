using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TBBProject.Core.Data.Migrations
{
    public partial class DynamicMenuCreatorMigrationV3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DynamicMenuItem");

            migrationBuilder.DropTable(
                name: "DynamicMenuResult");

            migrationBuilder.DropTable(
                name: "DynamicMenus");

            migrationBuilder.CreateTable(
                name: "DynamicQuestions",
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
                    table.PrimaryKey("PK_DynamicQuestions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DynamicQuestions_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DynamicQuestionsItem",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MenuType = table.Column<int>(nullable: false),
                    Values = table.Column<string>(nullable: true),
                    PropertyName = table.Column<string>(nullable: true),
                    DynamicQuestionsId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DynamicQuestionsItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DynamicQuestionsItem_DynamicQuestions_DynamicQuestionsId",
                        column: x => x.DynamicQuestionsId,
                        principalTable: "DynamicQuestions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DynamicQuestionsResult",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Result = table.Column<string>(nullable: true),
                    EnterDate = table.Column<DateTime>(nullable: false),
                    MyProperty = table.Column<long>(nullable: false),
                    DynamicQuestionsId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DynamicQuestionsResult", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DynamicQuestionsResult_DynamicQuestions_DynamicQuestionsId",
                        column: x => x.DynamicQuestionsId,
                        principalTable: "DynamicQuestions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DynamicQuestions_UserId",
                table: "DynamicQuestions",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_DynamicQuestionsItem_DynamicQuestionsId",
                table: "DynamicQuestionsItem",
                column: "DynamicQuestionsId");

            migrationBuilder.CreateIndex(
                name: "IX_DynamicQuestionsResult_DynamicQuestionsId",
                table: "DynamicQuestionsResult",
                column: "DynamicQuestionsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DynamicQuestionsItem");

            migrationBuilder.DropTable(
                name: "DynamicQuestionsResult");

            migrationBuilder.DropTable(
                name: "DynamicQuestions");

            migrationBuilder.CreateTable(
                name: "DynamicMenus",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: false)
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
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DynamicMenuId = table.Column<long>(type: "bigint", nullable: false),
                    MenuType = table.Column<int>(type: "int", nullable: false),
                    PropertyName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Values = table.Column<string>(type: "nvarchar(max)", nullable: true)
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

            migrationBuilder.CreateTable(
                name: "DynamicMenuResult",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DynamicMenuId = table.Column<long>(type: "bigint", nullable: false),
                    EnterDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MyProperty = table.Column<long>(type: "bigint", nullable: false),
                    Result = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                name: "IX_DynamicMenuItem_DynamicMenuId",
                table: "DynamicMenuItem",
                column: "DynamicMenuId");

            migrationBuilder.CreateIndex(
                name: "IX_DynamicMenuResult_DynamicMenuId",
                table: "DynamicMenuResult",
                column: "DynamicMenuId");

            migrationBuilder.CreateIndex(
                name: "IX_DynamicMenus_UserId",
                table: "DynamicMenus",
                column: "UserId");
        }
    }
}
