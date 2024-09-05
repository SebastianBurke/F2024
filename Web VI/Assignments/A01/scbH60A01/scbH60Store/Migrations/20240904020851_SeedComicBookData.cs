using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace scbH60Store.Migrations
{
    /// <inheritdoc />
    public partial class SeedComicBookData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "ProductCategory",
                columns: new[] { "CategoryID", "ProdCat" },
                values: new object[,]
                {
                    { 1, "Superhero" },
                    { 2, "Manga" },
                    { 3, "Graphic Novels" },
                    { 4, "Indie Comics" },
                    { 5, "Kids' Comics" }
                });

            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "ProductID", "BuyPrice", "Description", "Manufacturer", "ProdCatId", "SellPrice", "Stock" },
                values: new object[,]
                {
                    { 1, 10.00m, "Spider-Man: Homecoming", "Marvel", 1, 15.00m, 100 },
                    { 2, 8.00m, "Batman: The Killing Joke", "DC", 1, 12.00m, 50 },
                    { 3, 9.00m, "Wonder Woman: Blood", "DC", 1, 13.00m, 70 },
                    { 4, 12.00m, "The Avengers: Endgame", "Marvel", 1, 18.00m, 80 },
                    { 5, 5.00m, "Naruto: Volume 1", "Shonen Jump", 2, 7.00m, 120 },
                    { 6, 6.00m, "Attack on Titan: Volume 1", "Kodansha", 2, 8.00m, 90 },
                    { 7, 5.50m, "Dragon Ball Z: Volume 1", "Viz Media", 2, 8.50m, 110 },
                    { 8, 6.00m, "One Piece: Volume 1", "Shonen Jump", 2, 9.00m, 130 },
                    { 9, 15.00m, "Maus", "Pantheon", 3, 20.00m, 60 },
                    { 10, 18.00m, "Watchmen", "DC", 3, 25.00m, 40 },
                    { 11, 12.00m, "Persepolis", "Pantheon", 3, 17.00m, 30 },
                    { 12, 20.00m, "Sandman", "Vertigo", 3, 30.00m, 50 },
                    { 13, 10.00m, "Saga", "Image Comics", 4, 14.00m, 70 },
                    { 14, 9.00m, "The Walking Dead", "Image Comics", 4, 13.00m, 60 },
                    { 15, 11.00m, "Black Hammer", "Dark Horse", 4, 16.00m, 40 },
                    { 16, 13.00m, "Y: The Last Man", "Vertigo", 4, 18.00m, 50 },
                    { 17, 5.00m, "Dog Man", "Scholastic", 5, 8.00m, 80 },
                    { 18, 7.00m, "The Adventures of Tintin", "Little, Brown", 5, 10.00m, 70 },
                    { 19, 6.00m, "Bone", "Graphix", 5, 9.00m, 90 },
                    { 20, 8.00m, "Amulet", "Scholastic", 5, 12.00m, 100 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "ProductCategory");
        }
    }
}
