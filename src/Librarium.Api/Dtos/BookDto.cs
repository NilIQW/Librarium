namespace Librarium.Api.Dtos;

public record CreateBookRequest(string Title, string ISBN, int PublicationYear);

public record UpdateBookRequest(string Title, string ISBN, int PublicationYear);

public class BookDto
{
    public int BookId { get; set; }
    public string Title { get; set; } = null!;
    public string? ISBN { get; set; }
    public int PublicationYear { get; set; }
    public List<AuthorDto> Authors { get; set; } = new();
}