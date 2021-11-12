using Microsoft.EntityFrameworkCore.Migrations;

namespace DataImporter.Web.Migrations.SystemImporterDb
{
    public partial class OneToMany_GroupToFileStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GroupId",
                table: "FileStatusess",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_FileStatusess_GroupId",
                table: "FileStatusess",
                column: "GroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_FileStatusess_Groups_GroupId",
                table: "FileStatusess",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FileStatusess_Groups_GroupId",
                table: "FileStatusess");

            migrationBuilder.DropIndex(
                name: "IX_FileStatusess_GroupId",
                table: "FileStatusess");

            migrationBuilder.DropColumn(
                name: "GroupId",
                table: "FileStatusess");
        }
    }
}
