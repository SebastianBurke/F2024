using Microsoft.EntityFrameworkCore;
namespace PartB.Models;

public class MovieContext : DbContext
{
    public DbSet<Movie> Movies { get; set; }
    public DbSet<Genre> Genres { get; set; }
    public DbSet<MovieGenre> MovieGenres { get; set; }

    public MovieContext(DbContextOptions<MovieContext> options) : base(options)
    {
        Database.EnsureCreated();  // Comment this out for migrations
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
            new Movie { MovieId = 2, Title = "Titanic", Rating = 89.7m }
        );
    }
}
