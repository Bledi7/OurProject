using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OurProject.Migrations
{
    public partial class ImageChangeTOpHOTO : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Image",
                table: "Author",
                newName: "Photo");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Photo",
                table: "Author",
                newName: "Image");
        }
    }
}
