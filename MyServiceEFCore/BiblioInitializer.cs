using System;
using System.Linq;
using System.Threading.Tasks;
using Biblio.Domain;
using Biblio.Domain.Models;

public static class BiblioInitializer
{
    public static async Task InitializeAsync(LibraryContext db)
    {
        if (db.Books.Any()) return; // Déjà initialisé

        // === Catégories ===
        var categories = new[]
        {
            new Category { Name = "Science" },
            new Category { Name = "Histoire" },
            new Category { Name = "Roman" },
            new Category { Name = "Informatique" },
            new Category { Name = "Philosophie" }
        };
        db.Categories.AddRange(categories);

        // === Auteurs ===
        var authors = new[]
        {
            new Author { FirstName = "Alice", LastName = "Durand" },
            new Author { FirstName = "Bob", LastName = "Leroy" },
            new Author { FirstName = "Claire", LastName = "Nguyen" },
            new Author { FirstName = "David", LastName = "Zola" },
            new Author { FirstName = "Eva", LastName = "Moreau" }
        };
        db.Authors.AddRange(authors);

        // === Livres ===
        var books = Enumerable.Range(1, 10).Select(i => new Book
        {
            Title = $"Livre {i}",
            Type = i % 3 == 0 ? BookType.Scientifique :
                   i % 3 == 1 ? BookType.Narratif : BookType.Historique,
            Color = (BookColor)(i % Enum.GetValues(typeof(BookColor)).Length),
            Authors = { authors[i % authors.Length] },
            Categories = { categories[i % categories.Length] }
        }).ToList();

        db.Books.AddRange(books);
        await db.SaveChangesAsync();

        // === Utilisateurs ===
        var users = Enumerable.Range(1, 10).Select(i => new User
        {
            Name = $"User {i}",
            Email = $"user{i}@biblio.com"
        }).ToList();

        db.Users.AddRange(users);
        await db.SaveChangesAsync();

        // === Emprunts (22 emprunts fixes comme dans le sujet du TP4) ===
        var borrowings = new[]
        {
            new Borrowing { Book = books[0], User = users[0], BorrowDate = DateTime.Today.AddMonths(-10), ReturnDate = DateTime.Today.AddMonths(-8) },
            new Borrowing { Book = books[1], User = users[1], BorrowDate = DateTime.Today.AddMonths(-9), ReturnDate = DateTime.Today.AddMonths(-8) },
            new Borrowing { Book = books[2], User = users[2], BorrowDate = DateTime.Today.AddMonths(-8), ReturnDate = DateTime.Today.AddMonths(-7) },
            new Borrowing { Book = books[3], User = users[3], BorrowDate = DateTime.Today.AddMonths(-8), ReturnDate = DateTime.Today.AddMonths(-7) },
            new Borrowing { Book = books[4], User = users[4], BorrowDate = DateTime.Today.AddMonths(-7), ReturnDate = DateTime.Today.AddMonths(-6) },
            new Borrowing { Book = books[5], User = users[5], BorrowDate = DateTime.Today.AddMonths(-7), ReturnDate = DateTime.Today.AddMonths(-6) },
            new Borrowing { Book = books[6], User = users[6], BorrowDate = DateTime.Today.AddMonths(-6), ReturnDate = DateTime.Today.AddMonths(-5) },
            new Borrowing { Book = books[7], User = users[7], BorrowDate = DateTime.Today.AddMonths(-6), ReturnDate = DateTime.Today.AddMonths(-5) },
            new Borrowing { Book = books[8], User = users[8], BorrowDate = DateTime.Today.AddMonths(-5), ReturnDate = DateTime.Today.AddMonths(-4) },
            new Borrowing { Book = books[9], User = users[9], BorrowDate = DateTime.Today.AddMonths(-5), ReturnDate = DateTime.Today.AddMonths(-4) },
            new Borrowing { Book = books[0], User = users[1], BorrowDate = DateTime.Today.AddMonths(-4), ReturnDate = DateTime.Today.AddMonths(-3) },
            new Borrowing { Book = books[1], User = users[2], BorrowDate = DateTime.Today.AddMonths(-4), ReturnDate = DateTime.Today.AddMonths(-3) },
            new Borrowing { Book = books[2], User = users[3], BorrowDate = DateTime.Today.AddMonths(-3), ReturnDate = DateTime.Today.AddMonths(-2) },
            new Borrowing { Book = books[3], User = users[4], BorrowDate = DateTime.Today.AddMonths(-3), ReturnDate = DateTime.Today.AddMonths(-2) },
            new Borrowing { Book = books[4], User = users[5], BorrowDate = DateTime.Today.AddMonths(-3), ReturnDate = DateTime.Today.AddMonths(-2) },
            new Borrowing { Book = books[5], User = users[6], BorrowDate = DateTime.Today.AddMonths(-3), ReturnDate = DateTime.Today.AddMonths(-2) },
            new Borrowing { Book = books[6], User = users[7], BorrowDate = DateTime.Today.AddMonths(-3), ReturnDate = DateTime.Today.AddMonths(-1) },
            new Borrowing { Book = books[7], User = users[8], BorrowDate = DateTime.Today.AddMonths(-3), ReturnDate = DateTime.Today.AddMonths(-1) },
            new Borrowing { Book = books[8], User = users[9], BorrowDate = DateTime.Today.AddMonths(-2), ReturnDate = DateTime.Today.AddMonths(-1) },
            new Borrowing { Book = books[9], User = users[0], BorrowDate = DateTime.Today.AddMonths(-2), ReturnDate = DateTime.Today.AddMonths(-1) },
            new Borrowing { Book = books[1], User = users[3], BorrowDate = DateTime.Today.AddMonths(-2), ReturnDate = DateTime.Today.AddMonths(-1) },
            new Borrowing { Book = books[2], User = users[4], BorrowDate = DateTime.Today.AddMonths(-2), ReturnDate = DateTime.Today.AddMonths(-1) }
        };

        db.Borrowings.AddRange(borrowings);
        await db.SaveChangesAsync();
    }
}
