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
            var genres = await _context.Genres
                .Include(g => g.MovieGenres)
                .ThenInclude(mg => mg.Movie)
                .ToListAsync();

            // Create GenreStats for each genre
            var genreStatsList = genres.Select(g => new GenreStats(_context, g.GenreId)).ToList();

            return View(genreStatsList);
        }
    }
}
