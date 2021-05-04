using Microsoft.EntityFrameworkCore.Migrations;

namespace TBBProject.Core.Data.Migrations
{
    public partial class annanntype : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Announcements_AnnouncementType_AnnouncementTypeId",
                table: "Announcements");

            migrationBuilder.DropIndex(
                name: "IX_Announcements_AnnouncementTypeId",
                table: "Announcements");

            migrationBuilder.DropColumn(
                name: "AnnouncementTypeId",
                table: "Announcements");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "AnnouncementTypeId",
                table: "Announcements",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Announcements_AnnouncementTypeId",
                table: "Announcements",
                column: "AnnouncementTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Announcements_AnnouncementType_AnnouncementTypeId",
                table: "Announcements",
                column: "AnnouncementTypeId",
                principalTable: "AnnouncementType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
