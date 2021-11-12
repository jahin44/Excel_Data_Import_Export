using Microsoft.EntityFrameworkCore.Migrations;

namespace DataImporter.Web.Data.Migrations
{
    public partial class FileUrl_Entity_Add : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FileName",
                table: "FileStatusess",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileName",
                table: "FileStatusess");
        }
    }
}
