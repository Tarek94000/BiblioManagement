using System.Collections.Generic;

namespace Biblio.Domain.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        
        // Navigation : une catégorie peut contenir plusieurs livres
        public ICollection<Book> Books { get; set; } = new List<Book>();
    }
}
