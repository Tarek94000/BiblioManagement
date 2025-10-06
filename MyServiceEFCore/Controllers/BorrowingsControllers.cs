using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Biblio.Domain;
using Biblio.Domain.Models;
using MyServiceEFCore.Services;


namespace MyServiceEFCore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BorrowingsController : ControllerBase
    {
        private readonly LibraryContext _db;

        private readonly BorrowingSimulator _simulator;

        public BorrowingsController(LibraryContext db, BorrowingSimulator simulator)
        {
            _db = db;
            _simulator = simulator;
        }


        // GET: api/Borrowings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Borrowing>>> GetAll()
        {
            return await _db.Borrowings
                .Include(b => b.Book)
                .Include(b => b.User)
                .ToListAsync();
        }

        // POST: api/Borrowings
        [HttpPost]
        public async Task<ActionResult<Borrowing>> Create(Borrowing borrowing)
        {
            _db.Borrowings.Add(borrowing);
            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(GetAll), new { id = borrowing.Id }, borrowing);
        }

        // 🧮 1️⃣ Livre(s) le(s) plus emprunté(s)
        [HttpGet("TopBooks")]
        public async Task<IActionResult> GetTopBooks()
        {
            var top = await _db.Borrowings
                .GroupBy(b => b.Book.Title)
                .Select(g => new { Book = g.Key, Count = g.Count() })
                .OrderByDescending(x => x.Count)
                .Take(5)
                .ToListAsync();

            return Ok(top);
        }

        // 🧮 2️⃣ Durée moyenne d’emprunt par catégorie
        [HttpGet("AverageDuration")]
        public async Task<IActionResult> GetAverageDurationByCategory()
        {
            var stats = await _db.Borrowings
                .Include(b => b.Book)
                .ThenInclude(book => book.Categories)
                .SelectMany(b => b.Book.Categories.Select(c => new
                {
                    Category = c.Name,
                    Duration = (b.ReturnDate - b.BorrowDate).TotalDays
                }))
                .GroupBy(x => x.Category)
                .Select(g => new { Category = g.Key, AverageDays = g.Average(x => x.Duration) })
                .ToListAsync();

            return Ok(stats);
        }

        // 🧮 3️⃣ Utilisateurs ayant emprunté plus de X livres
        [HttpGet("ActiveUsers/{minCount:int}")]
        public async Task<IActionResult> GetActiveUsers(int minCount)
        {
            var active = await _db.Borrowings
                .GroupBy(b => b.User.Name)
                .Select(g => new { User = g.Key, BorrowCount = g.Count() })
                .Where(x => x.BorrowCount > minCount)
                .OrderByDescending(x => x.BorrowCount)
                .ToListAsync();

            return Ok(active);
        }

        // 🧮 4️⃣ Groupement des emprunts par mois
        [HttpGet("ByMonth")]
        public async Task<IActionResult> GetBorrowingsByMonth()
        {
            var byMonth = await _db.Borrowings
                .GroupBy(b => new { b.BorrowDate.Year, b.BorrowDate.Month })
                .Select(g => new
                {
                    Year = g.Key.Year,
                    Month = g.Key.Month,
                    Count = g.Count()
                })
                .OrderBy(g => g.Year)
                .ThenBy(g => g.Month)
                .ToListAsync();

            return Ok(byMonth);
        }

        [HttpPost("Simulate")]
        public async Task<IActionResult> Simulate()
        {
            await _simulator.SimulateConcurrentBorrowings();
            return Ok("Simulation terminée avec succès !");
        }

    }
}
