using Librarium.Data.Entities;
using Librarium.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Librarium.Data.Repositories.Implementations;

public class BookRepository : IBookRepository
{
    private readonly LibrariumDbContext _context;

    public BookRepository(LibrariumDbContext context)
    {
        _context = context;
    }

    public async Task<List<Book>> GetAllAsync()
    {
        return await _context.Books
            .Include(b => b.Authors)
            .ToListAsync();
    }
}