using System.Linq;
using System.Threading.Tasks;
using Biblio.Domain;
using Biblio.Domain.Models;

public static class BiblioInitializer
{
    public static async Task InitializeAsync(LibraryContext db)
    {
        if (db.Books.Any()) return;

        var categories = new[]
        {
            new Category { Name = "Science" },
            new Category { Name = "Histoire" },
            new Category { Name = "Roman" },
            new Category { Name = "Informatique" },
            new Category { Name = "Philosophie" }
        };

        var authors = new[]
        {
            new Author { FirstName = "Alice", LastName = "Durand" },
            new Author { FirstName = "Bob", LastName = "Leroy" },
            new Author { FirstName = "Claire", LastName = "Nguyen" },
            new Author { FirstName = "David", LastName = "Zola" },
            new Author { FirstName = "Eva", LastName = "Moreau" }
        };

        db.Categories.AddRange(categories);
        db.Authors.AddRange(authors);

        var books = Enumerable.Range(1, 10).Select(i => new Book
        {
            Title = $"Livre {i}",
            Type = i % 3 == 0 ? BookType.Scientifique :
                   i % 3 == 1 ? BookType.Narratif : BookType.Historique,
            Authors = { authors[i % authors.Length] },
            Categories = { categories[i % categories.Length] }
        });

        db.Books.AddRange(books);
        await db.SaveChangesAsync();
    }
}
