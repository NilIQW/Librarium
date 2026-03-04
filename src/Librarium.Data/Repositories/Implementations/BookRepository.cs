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

    public async Task<Book> CreateAsync(Book book)
    {
        _context.Books.Add(book);
        await _context.SaveChangesAsync();
        return book;
    }

    public async Task<List<Book>> GetAllAsync()
    {
        return await _context.Books
            .Include(b => b.Authors)
            .ToListAsync();
    }

    public async Task<Book?> GetByIdAsync(int id)
    {
        return await _context.Books
            .Include(b => b.Authors)
            .FirstOrDefaultAsync(b => b.BookId == id);
    }

    public async Task<Book> UpdateAsync(Book book)
    {
        _context.Books.Update(book);
        await _context.SaveChangesAsync();
        return book;
    }

    public async Task DeleteAsync(Book book)
    {
        _context.Books.Remove(book);
        await _context.SaveChangesAsync();
    }
}