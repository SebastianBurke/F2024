using Microsoft.EntityFrameworkCore;
using PartB.Models;
using System;

namespace PartBTest
{
    public class UnitTest1
    {
        private DbContextOptions<MovieContext> GetInMemoryDbContextOptions()
        {
            return new DbContextOptionsBuilder<MovieContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
        }

        //[Fact]
        //public void GenreStats_Constructor_ShouldThrowNotImplementedException()
        //{
        //    // Arrange
        //    var genre = new Genre
        //    {
        //        GenreId = 1,
        //        GenreName = "Comedy",
        //        MovieGenres = new List<MovieGenre>() // Can be left empty for this test
        //    };

        //    // Act & Assert
        //    Assert.Throws<NotImplementedException>(() => new GenreStats(genre));
        //}

        [Theory]
        [InlineData("Comedy", 2, 4.5)] 
        [InlineData("Drama", 3, 3.67)] 
        [InlineData("Action", 0, 0)] 
        public void GenreStats_ShouldCalculateMovieCountAndAvgRating(string genreName, int expectedCount, decimal expectedAvgRating)
        {
            // Arrange
            var options = GetInMemoryDbContextOptions();

            using (var context = new MovieContext(options))
            {
                // Create a genre with no movies if movie count is 0
                var genre = new Genre
                {
                    GenreName = genreName,
                    MovieGenres = expectedCount > 0
                        ? new List<MovieGenre>
                        {
                    new MovieGenre { Movie = new Movie { Title = "Movie1", Rating = 4.0m } },
                    new MovieGenre { Movie = new Movie { Title = "Movie2", Rating = 5.0m } },
                    genreName == "Drama" ? new MovieGenre { Movie = new Movie { Title = "Movie3", Rating = 2.0m } } : null
                        }.Where(mg => mg != null).ToList()
                        : new List<MovieGenre>() // No movies for the genre
                };

                context.Genres.Add(genre);
                context.SaveChanges();

                // Act
                var genreStats = new GenreStats(genre);

                // Assert
                Assert.Equal(expectedCount, genreStats.MovieCount);
                Assert.Equal(expectedAvgRating, genreStats.AvgRating);
            }
        }

    }
}