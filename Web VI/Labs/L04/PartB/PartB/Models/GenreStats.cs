using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace PartB.Models
{
    public class GenreStats
    {
        public string GenreName { get; private set; }
        public int Count { get; private set; }
        public decimal AvgRating { get; private set; }

        // Constructor that accepts MovieContext and genreId
        public GenreStats(MovieContext context, long genreId)
        {
            var genre = context.Genres
                .Include(g => g.MovieGenres)
                .ThenInclude(mg => mg.Movie)
                .FirstOrDefault(g => g.GenreId == genreId);

            if (genre == null)
            {
                throw new System.Exception("Genre not found");
            }

            GenreName = genre.GenreName;
            Count = genre.MovieGenres.Count;

            AvgRating = Count > 0
                ? genre.MovieGenres.Average(mg => mg.Movie.Rating)
                : 0m;
        }
    }
}
