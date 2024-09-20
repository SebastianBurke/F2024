using Microsoft.EntityFrameworkCore;
namespace PartB.Models;

public class MovieContext : DbContext
{
    public DbSet<Movie> Movies { get; set; }
    public DbSet<Genre> Genres { get; set; }
    public DbSet<MovieGenre> MovieGenres { get; set; }

    public MovieContext(DbContextOptions<MovieContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Genre>().HasData(
            new Genre { GenreId = 1, GenreName = "Action" },
            new Genre { GenreId = 2, GenreName = "Comedy" },
            new Genre { GenreId = 3, GenreName = "Drama" },
            new Genre { GenreId = 4, GenreName = "Horror" },
            new Genre { GenreId = 5, GenreName = "Sci-Fi" }
        );

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
    }
}
