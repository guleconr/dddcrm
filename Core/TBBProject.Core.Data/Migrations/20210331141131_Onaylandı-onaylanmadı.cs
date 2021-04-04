using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TBBProject.Core.Data.Migrations
{
    public partial class Onaylandıonaylanmadı : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ApprovalStatus",
                table: "News",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ApprovalStatus",
                table: "Announcements",
                nullable: false,
                defaultValue: 0);

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApprovalStatus",
                table: "News");

            migrationBuilder.DropColumn(
                name: "ApprovalStatus",
                table: "Announcements");
        }
    }
}
