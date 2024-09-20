using Microsoft.AspNetCore.Mvc;
using PartB.Models;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace PartB.Controllers
{
    public class GenresController : Controller
    {
        private readonly MovieContext _context;

        public GenresController(MovieContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            // Ensure all relationships are eagerly loaded
            var genres = await _context.Genres
                .Include(g => g.MovieGenres)
                .ThenInclude(mg => mg.Movie) // Make sure movies are loaded with genres
                .ToListAsync();

            // Check if the genres and related movies are loaded
            if (genres == null || !genres.Any())
            {
                return View(new List<GenreStats>()); // Return empty list if no genres are found
            }

            // Create GenreStats for each genre
            var genreStatsList = genres.Select(g => new GenreStats(_context, g.GenreId)).ToList();

            // Log the genre stats to verify
            foreach (var stats in genreStatsList)
            {
                Console.WriteLine($"Genre: {stats.GenreName}, Count: {stats.Count}, AvgRating: {stats.AvgRating}");
            }

            return View(genreStatsList);


            // Pass the list to the view
            return View(genreStatsList);
        }
    }
}
