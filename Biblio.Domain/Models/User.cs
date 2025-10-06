namespace Biblio.Domain.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string? Email { get; set; }
        
        // Navigation
        public ICollection<Borrowing> Borrowings { get; set; } = new List<Borrowing>();
    }
}
