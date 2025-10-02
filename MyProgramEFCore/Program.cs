using System;
using System.Linq;
using Biblio.Domain;
using Biblio.Domain.Models;
using Microsoft.EntityFrameworkCore;

class Program
{
    static void Main()
    {
        // 👉 Ici on fabrique les options pour dire à EF Core d'utiliser une base InMemory
        var options = new DbContextOptionsBuilder<LibraryContext>()
            .UseInMemoryDatabase("BiblioDbConsole")
            .Options;

        using var db = new LibraryContext(options);

        // === Ajout de données si base vide ===
        if (!db.Books.Any())
        {
            var sci = new Category { Name = "Science" };
            var author = new Author { FirstName = "Marie", LastName = "Curie" };

            db.Books.Add(new Book
            {
                Title = "Radiations",
                Type = BookType.Scientifique,
                Authors = { author },
                Categories = { sci }
            });

            db.SaveChanges();
        }

        // === Lecture et affichage ===
        Console.WriteLine("📚 Livres enregistrés :");
        foreach (var book in db.Books.Include(b => b.Authors).Include(b => b.Categories))
        {
            Console.WriteLine($"{book.Id}: {book.Title} ({book.Type})");
        }
    }
}
