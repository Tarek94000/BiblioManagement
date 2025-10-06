using System.Collections.Generic;

namespace Biblio.Domain.Models
{
    public enum BookColor
    {
        Red, Blue, Green, Yellow, Black, White
    }

    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; } = default!;
        public BookType Type { get; set; }
        public BookColor Color { get; set; }

        // Navigation : un livre peut avoir plusieurs auteurs et catégories
        public ICollection<Author> Authors { get; set; } = new List<Author>();
        public ICollection<Category> Categories { get; set; } = new List<Category>();
    }
}
