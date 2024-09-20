namespace PartB.Models
{
    public class GenreStats
    {
        public long GenreId { get; set; }
        public string GenreName { get; set; }
        public int MovieCount { get; set; }
        public decimal AvgRating { get; set; }

        public GenreStats(Genre genre)
        {
            GenreId = genre.GenreId;
            GenreName = genre.GenreName;

            MovieCount = genre.MovieGenres.Count;
            AvgRating = genre.MovieGenres.Any()
                ? Math.Round(genre.MovieGenres.Average(mg => mg.Movie.Rating), 2)
                : 0m;
        }
    }
}
