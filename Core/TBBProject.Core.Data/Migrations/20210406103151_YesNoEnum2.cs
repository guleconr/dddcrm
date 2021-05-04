using Microsoft.EntityFrameworkCore.Migrations;

namespace TBBProject.Core.Data.Migrations
{
    public partial class YesNoEnum2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "IsRelease",
                table: "News",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "IsRelease",
                table: "News",
                type: "bit",
                nullable: false,
                oldClrType: typeof(int));
        }
    }
}
