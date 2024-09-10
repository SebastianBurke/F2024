using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace scbH60Store.Migrations
{
    /// <inheritdoc />
    public partial class AddCategoryImage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "ProductCategory",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "ProductCategory",
                keyColumn: "CategoryID",
                keyValue: 1,
                column: "ImageUrl",
                value: "cat_superhero.jpg");

            migrationBuilder.UpdateData(
                table: "ProductCategory",
                keyColumn: "CategoryID",
                keyValue: 2,
                column: "ImageUrl",
                value: "cat_manga.jpg");

            migrationBuilder.UpdateData(
                table: "ProductCategory",
                keyColumn: "CategoryID",
                keyValue: 3,
                column: "ImageUrl",
                value: "cat_graphic_novels.jpg");

            migrationBuilder.UpdateData(
                table: "ProductCategory",
                keyColumn: "CategoryID",
                keyValue: 4,
                column: "ImageUrl",
                value: "cat_indie_comics.jpg");

            migrationBuilder.UpdateData(
                table: "ProductCategory",
                keyColumn: "CategoryID",
                keyValue: 5,
                column: "ImageUrl",
                value: "cat_kids_comics.jpg");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "ProductCategory");
        }
    }
}
