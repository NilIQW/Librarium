using Librarium.Api.Dtos;

namespace Librarium.Api.Services.Interfaces;

public interface IAuthorService
{
    Task<AuthorDto> CreateAsync(CreateAuthorRequest request);
}