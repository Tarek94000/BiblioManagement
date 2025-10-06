namespace Biblio.Domain.Models
{
    public class Borrowing
    {
        public int Id { get; set; }
        public Book Book { get; set; } = default!;
        public User User { get; set; } = default!;
        public DateTime BorrowDate { get; set; }
        public DateTime ReturnDate { get; set; }
    }
}
