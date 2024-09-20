namespace PartB.Models;
using System.Collections.Generic;

public class Genre
{
    public long GenreId { get; set; }
    public string GenreName { get; set; }
    public ICollection<MovieGenre> MovieGenres { get; set; }
}
