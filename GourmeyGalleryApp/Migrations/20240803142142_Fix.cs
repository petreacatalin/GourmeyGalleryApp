using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GourmeyGalleryApp.Migrations
{
    /// <inheritdoc />
    public partial class Fix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Step");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Step",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
