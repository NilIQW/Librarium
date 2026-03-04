using Librarium.Data.Entities;

namespace Librarium.Data.Repositories.Interfaces;

public interface IAuthorRepository
{
    Task<Author> CreateAsync(Author author);
}