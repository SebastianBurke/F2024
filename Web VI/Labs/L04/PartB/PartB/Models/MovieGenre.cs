namespace PartB.Models;
public class MovieGenre
{
    public long MovieGenreId { get; set; }
    public long GenreId { get; set; }
    public long MovieId { get; set; }
    public Movie Movie { get; set; }
    public Genre Genre { get; set; }

}
