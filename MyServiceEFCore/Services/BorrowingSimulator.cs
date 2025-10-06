using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Biblio.Domain;
using Biblio.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MyServiceEFCore.Services
{
    public class BorrowingSimulator
    {
        private readonly LibraryContext _context;
        private readonly ILogger<BorrowingSimulator> _logger;
        private readonly object _lock = new();

        public BorrowingSimulator(LibraryContext context, ILogger<BorrowingSimulator> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task SimulateConcurrentBorrowings()
        {
            var tasks = new List<Task>();

            for (int i = 0; i < 10; i++)
            {
                tasks.Add(Task.Run(() =>
                {
                    lock (_lock)
                    {
                        var user = _context.Users.OrderBy(u => Guid.NewGuid()).FirstOrDefault();
                        var book = _context.Books.OrderBy(b => Guid.NewGuid()).FirstOrDefault();

                        if (user != null && book != null)
                        {
                            var borrowing = new Borrowing
                            {
                                User = user,
                                Book = book,
                                BorrowDate = DateTime.Now,
                                ReturnDate = DateTime.Now.AddDays(7)
                            };

                            _context.Borrowings.Add(borrowing);
                            _context.SaveChanges();

                            _logger.LogInformation($"📚 Emprunt simulé : {user.Name} a emprunté '{book.Title}'");
                        }
                    }
                }));
            }

            await Task.WhenAll(tasks);
            _logger.LogInformation("✅ Simulation terminée : tous les emprunts ont été enregistrés.");
        }
    }
}
