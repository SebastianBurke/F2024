using Microsoft.AspNetCore.Mvc;
using PartB.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using System;

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

            var genreStatsList = genres.Select(g => new GenreStats(g)).ToList();

            return View(genreStatsList);
        }
    }
}
