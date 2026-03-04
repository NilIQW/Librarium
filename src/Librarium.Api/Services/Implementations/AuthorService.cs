using Librarium.Api.Dtos;
using Librarium.Api.Services.Interfaces;
using Librarium.Data.Entities;
using Librarium.Data.Repositories.Interfaces;

namespace Librarium.Api.Services.Implementations;

public class AuthorService : IAuthorService
{
    private readonly IAuthorRepository _repository;

    public AuthorService(IAuthorRepository repository)
    {
        _repository = repository;
    }

    public async Task<AuthorDto> CreateAsync(CreateAuthorRequest request)
    {
        var author = new Author
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Biography = request.Biography
        };

        var created = await _repository.CreateAsync(author);

        return new AuthorDto(
            created.AuthorId,
            created.FirstName,
            created.LastName,
            created.Biography
        );
    }
}