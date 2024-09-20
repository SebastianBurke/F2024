using Microsoft.EntityFrameworkCore;
namespace PartB.Models;

public class MovieContext : DbContext
{
    public DbSet<Movie> Movies { get; set; }
    public DbSet<Genre> Genres { get; set; }
    public DbSet<MovieGenre> MovieGenres { get; set; }

    public MovieContext(DbContextOptions<MovieContext> options) : base(options)
    {
        //Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Seed Genres
        modelBuilder.Entity<Genre>().HasData(
            new Genre { GenreId = 1, GenreName = "Action" },
            new Genre { GenreId = 2, GenreName = "Comedy" },
            new Genre { GenreId = 3, GenreName = "Drama" },
            new Genre { GenreId = 4, GenreName = "Horror" },
            new Genre { GenreId = 5, GenreName = "Sci-Fi" },
            new Genre { GenreId = 6, GenreName = "Romance" } // Added Romance genre
        );

        // Seed Movies
        modelBuilder.Entity<Movie>().HasData(
            new Movie { MovieId = 1, Title = "Inception", Rating = 88.5m },
            new Movie { MovieId = 2, Title = "Titanic", Rating = 89.7m },
            new Movie { MovieId = 3, Title = "Inception 2", Rating = 88.5m },
            new Movie { MovieId = 4, Title = "Titanic 2", Rating = 89.7m },
            new Movie { MovieId = 5, Title = "Inception 3", Rating = 88.5m },
            new Movie { MovieId = 6, Title = "Titanic 3", Rating = 89.7m },
            new Movie { MovieId = 7, Title = "Inception 4", Rating = 88.5m },
            new Movie { MovieId = 8, Title = "Titanic 4", Rating = 89.7m },
            new Movie { MovieId = 9, Title = "Inception 5", Rating = 88.5m },
            new Movie { MovieId = 10, Title = "Titanic 5 ", Rating = 89.7m },
            new Movie { MovieId = 11, Title = "Inception 6", Rating = 88.5m },
            new Movie { MovieId = 12, Title = "Titanic 6", Rating = 89.7m }
        );

        // Seed MovieGenres (link movies to genres)
        modelBuilder.Entity<MovieGenre>().HasData(
            // Inception series -> Action, Sci-Fi
            new MovieGenre { MovieGenreId = 1, MovieId = 1, GenreId = 1 }, // Inception -> Action
            new MovieGenre { MovieGenreId = 2, MovieId = 1, GenreId = 5 }, // Inception -> Sci-Fi
            new MovieGenre { MovieGenreId = 3, MovieId = 3, GenreId = 1 }, // Inception 2 -> Action
            new MovieGenre { MovieGenreId = 4, MovieId = 3, GenreId = 5 }, // Inception 2 -> Sci-Fi
            new MovieGenre { MovieGenreId = 5, MovieId = 5, GenreId = 1 }, // Inception 3 -> Action
            new MovieGenre { MovieGenreId = 6, MovieId = 5, GenreId = 5 }, // Inception 3 -> Sci-Fi
            new MovieGenre { MovieGenreId = 7, MovieId = 7, GenreId = 1 }, // Inception 4 -> Action
            new MovieGenre { MovieGenreId = 8, MovieId = 7, GenreId = 5 }, // Inception 4 -> Sci-Fi
            new MovieGenre { MovieGenreId = 9, MovieId = 9, GenreId = 1 }, // Inception 5 -> Action
            new MovieGenre { MovieGenreId = 10, MovieId = 9, GenreId = 5 }, // Inception 5 -> Sci-Fi
            new MovieGenre { MovieGenreId = 11, MovieId = 11, GenreId = 1 }, // Inception 6 -> Action
            new MovieGenre { MovieGenreId = 12, MovieId = 11, GenreId = 5 }, // Inception 6 -> Sci-Fi

            // Titanic series -> Drama, Romance
            new MovieGenre { MovieGenreId = 13, MovieId = 2, GenreId = 3 }, // Titanic -> Drama
            new MovieGenre { MovieGenreId = 14, MovieId = 2, GenreId = 6 }, // Titanic -> Romance
            new MovieGenre { MovieGenreId = 15, MovieId = 4, GenreId = 3 }, // Titanic 2 -> Drama
            new MovieGenre { MovieGenreId = 16, MovieId = 4, GenreId = 6 }, // Titanic 2 -> Romance
            new MovieGenre { MovieGenreId = 17, MovieId = 6, GenreId = 3 }, // Titanic 3 -> Drama
            new MovieGenre { MovieGenreId = 18, MovieId = 6, GenreId = 6 }, // Titanic 3 -> Romance
            new MovieGenre { MovieGenreId = 19, MovieId = 8, GenreId = 3 }, // Titanic 4 -> Drama
            new MovieGenre { MovieGenreId = 20, MovieId = 8, GenreId = 6 }, // Titanic 4 -> Romance
            new MovieGenre { MovieGenreId = 21, MovieId = 10, GenreId = 3 }, // Titanic 5 -> Drama
            new MovieGenre { MovieGenreId = 22, MovieId = 10, GenreId = 6 }, // Titanic 5 -> Romance
            new MovieGenre { MovieGenreId = 23, MovieId = 12, GenreId = 3 }, // Titanic 6 -> Drama
            new MovieGenre { MovieGenreId = 24, MovieId = 12, GenreId = 6 }  // Titanic 6 -> Romance
        );
    }

}
