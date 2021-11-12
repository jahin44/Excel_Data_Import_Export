using Microsoft.EntityFrameworkCore.Migrations;

namespace DataImporter.Web.Migrations.SystemImporterDb
{
    public partial class Delete_GroupID : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GroupId",
                table: "FileStatusess");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GroupId",
                table: "FileStatusess",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
