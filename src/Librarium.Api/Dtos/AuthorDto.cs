namespace Librarium.Api.Dtos;

public record CreateAuthorRequest(string FirstName, string LastName, string? Biography);

public record AuthorDto(int AuthorId, string FirstName, string LastName, string? Biography);