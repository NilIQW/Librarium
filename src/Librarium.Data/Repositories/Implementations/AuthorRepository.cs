using Librarium.Data.Entities;
using Librarium.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Librarium.Data.Repositories.Implementations;

public class AuthorRepository : IAuthorRepository
{
    private readonly LibrariumDbContext _context;

    public AuthorRepository(LibrariumDbContext context)
    {
        _context = context;
    }

    

    public async Task<Author> CreateAsync(Author author)
    {
        _context.Authors.Add(author);
        await _context.SaveChangesAsync();
        return author;
    }

    public async Task<List<Author>> GetByIdsAsync(List<int> authorIds)
    {
        return await _context.Authors
            .Where(a => authorIds.Contains(a.AuthorId))
            .ToListAsync();
    }
}