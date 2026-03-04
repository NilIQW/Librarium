using Librarium.Data.Entities;

namespace Librarium.Data.Repositories.Interfaces;

public interface IBookRepository
{
    Task<List<Book>> GetAllAsync();
}