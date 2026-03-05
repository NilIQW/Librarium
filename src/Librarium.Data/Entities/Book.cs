namespace Librarium.Data.Entities;

public class Book
{
    public int BookId { get; set; }
    public string Title { get; set; } = null!;
    public string? IsbnText { get; set; }
    public int PublicationYear { get; set; }
    public bool IsRetired { get; set; } = false;
    public ICollection<Loan> Loans { get; set; } = new List<Loan>();
    public ICollection<Author> Authors { get; set; } = new List<Author>();

}