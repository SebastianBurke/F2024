using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PartB.Migrations
{
    /// <inheritdoc />
    public partial class _1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Genres",
                columns: table => new
                {
                    GenreId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GenreName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genres", x => x.GenreId);
                });

            migrationBuilder.CreateTable(
                name: "Movies",
                columns: table => new
                {
                    MovieId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rating = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movies", x => x.MovieId);
                });

            migrationBuilder.CreateTable(
                name: "MovieGenres",
                columns: table => new
                {
                    MovieGenreId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GenreId = table.Column<long>(type: "bigint", nullable: false),
                    MovieId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieGenres", x => x.MovieGenreId);
                    table.ForeignKey(
                        name: "FK_MovieGenres_Genres_GenreId",
                        column: x => x.GenreId,
                        principalTable: "Genres",
                        principalColumn: "GenreId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MovieGenres_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "MovieId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "GenreId", "GenreName" },
                values: new object[,]
                {
                    { 1L, "Action" },
                    { 2L, "Comedy" },
                    { 3L, "Drama" },
                    { 4L, "Horror" },
                    { 5L, "Sci-Fi" },
                    { 6L, "Romance" }
                });

            migrationBuilder.InsertData(
                table: "Movies",
                columns: new[] { "MovieId", "Rating", "Title" },
                values: new object[,]
                {
                    { 1L, 88.5m, "Inception" },
                    { 2L, 89.7m, "Titanic" },
                    { 3L, 88.5m, "Inception 2" },
                    { 4L, 89.7m, "Titanic 2" },
                    { 5L, 88.5m, "Inception 3" },
                    { 6L, 89.7m, "Titanic 3" },
                    { 7L, 88.5m, "Inception 4" },
                    { 8L, 89.7m, "Titanic 4" },
                    { 9L, 88.5m, "Inception 5" },
                    { 10L, 89.7m, "Titanic 5 " },
                    { 11L, 88.5m, "Inception 6" },
                    { 12L, 89.7m, "Titanic 6" }
                });

            migrationBuilder.InsertData(
                table: "MovieGenres",
                columns: new[] { "MovieGenreId", "GenreId", "MovieId" },
                values: new object[,]
                {
                    { 1L, 1L, 1L },
                    { 2L, 5L, 1L },
                    { 3L, 1L, 3L },
                    { 4L, 5L, 3L },
                    { 5L, 1L, 5L },
                    { 6L, 5L, 5L },
                    { 7L, 1L, 7L },
                    { 8L, 5L, 7L },
                    { 9L, 1L, 9L },
                    { 10L, 5L, 9L },
                    { 11L, 1L, 11L },
                    { 12L, 5L, 11L },
                    { 13L, 3L, 2L },
                    { 14L, 6L, 2L },
                    { 15L, 3L, 4L },
                    { 16L, 6L, 4L },
                    { 17L, 3L, 6L },
                    { 18L, 6L, 6L },
                    { 19L, 3L, 8L },
                    { 20L, 6L, 8L },
                    { 21L, 3L, 10L },
                    { 22L, 6L, 10L },
                    { 23L, 3L, 12L },
                    { 24L, 6L, 12L }
                });

            migrationBuilder.CreateIndex(
                name: "IX_MovieGenres_GenreId",
                table: "MovieGenres",
                column: "GenreId");

            migrationBuilder.CreateIndex(
                name: "IX_MovieGenres_MovieId",
                table: "MovieGenres",
                column: "MovieId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MovieGenres");

            migrationBuilder.DropTable(
                name: "Genres");

            migrationBuilder.DropTable(
                name: "Movies");
        }
    }
}
