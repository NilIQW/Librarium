using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Librarium.Data;

namespace Librarium.Api.Controllers;

[ApiController]
[Route("api/books")]
public class BooksController : ControllerBase
{
    private readonly LibrariumDbContext _context;

    public BooksController(LibrariumDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetBooks()
    {
        var books = await _context.Books
            .Include(b => b.Authors)
            .Select(b => new {
                b.BookId,
                b.Title,
                b.ISBN,
                b.PublicationYear,
                Authors = b.Authors.Select(a => new {
                    a.AuthorId,
                    a.FirstName,
                    a.LastName,
                    a.Biography
                }).ToList()
            })
            .ToListAsync();

        return Ok(books);
    }
}