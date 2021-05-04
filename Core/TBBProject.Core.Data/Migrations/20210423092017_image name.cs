using Microsoft.EntityFrameworkCore.Migrations;

namespace TBBProject.Core.Data.Migrations
{
    public partial class imagename : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NewsImageName",
                table: "NewsLang",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SliderImageName",
                table: "NewsLang",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NewsImageName",
                table: "NewsLang");

            migrationBuilder.DropColumn(
                name: "SliderImageName",
                table: "NewsLang");
        }
    }
}
