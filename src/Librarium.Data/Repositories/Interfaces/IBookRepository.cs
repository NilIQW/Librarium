using Librarium.Data.Entities;

namespace Librarium.Data.Repositories.Interfaces;

public interface IBookRepository
{
    Task<Book> CreateAsync(Book book);
    Task<List<Book>> GetAllAsync();
    Task<Book?> GetByIdAsync(int id);
    Task<Book> UpdateAsync(Book book);
    Task DeleteAsync(Book book);
}