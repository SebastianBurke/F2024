using Microsoft.EntityFrameworkCore;
using PartB.Models;

namespace DatabaseContextTest
{
    public class GenreStatsTestsInMemoryDb
    {
        [Theory]
        // (Genre Id, Count, AvgRating)
        [InlineData(1, 2, (90.0 + 85.0) / 2)]
        [InlineData(4, 0, 0.0)]
        public void GetGenreStatesInMemoryDB(long genreId, int expectedCount, decimal expectedAvgRating)
        {
            // Arrange.
            var contextOptions = (
                new DbContextOptionsBuilder<MovieContext>()
                .UseInMemoryDatabase(databaseName: "testDb_L03b")
                .Options
            );

            // Seed test data.
            var _db = new MovieContext(contextOptions);
            _db.Database.EnsureDeleted();

            _db.Genres.Add(new Genre { GenreId = 1, GenreName = "Action" });
            _db.Genres.Add(new Genre { GenreId = 2, GenreName = "Science-Fiction" });
            _db.Genres.Add(new Genre { GenreId = 3, GenreName = "Adventure" });
            _db.Genres.Add(new Genre { GenreId = 4, GenreName = "Romance" });

            _db.Movies.Add(new Movie { MovieId = 1, Title = "The Incredibles", Rating = 90.0m });
            _db.Movies.Add(new Movie { MovieId = 2, Title = "The Super Mario Bros. Movie", Rating = 85.0m });
            _db.Movies.Add(new Movie { MovieId = 3, Title = "WALL-E", Rating = 90.0m });

            _db.MovieGenres.Add(new MovieGenre { MovieGenreId = 1, MovieId = 1, GenreId = 1 });
            _db.MovieGenres.Add(new MovieGenre { MovieGenreId = 2, MovieId = 2, GenreId = 1 });
            _db.MovieGenres.Add(new MovieGenre { MovieGenreId = 3, MovieId = 2, GenreId = 3 });
            _db.MovieGenres.Add(new MovieGenre { MovieGenreId = 4, MovieId = 3, GenreId = 2 });

            _db.SaveChanges();

            // Act.
            try
            {
                GenreStats genreStats = new(_db, genreId);

                Assert.Equal(expectedCount, genreStats.Count);
                Assert.Equal(expectedAvgRating, genreStats.AvgRating);
            }
            catch
            {
                Assert.False(true); // NOT OK!
            }

            // Assert.
            Assert.True(true); // OK!
        }
    }
}