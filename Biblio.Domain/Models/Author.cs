using System.Collections.Generic;

namespace Biblio.Domain.Models
{
    public class Author
    {
        public int Id { get; set; }
        public string LastName { get; set; } = default!;
        public string FirstName { get; set; } = default!;
        
        // Navigation : un auteur peut écrire plusieurs livres
        public ICollection<Book> Books { get; set; } = new List<Book>();
    }
}
