using Librarium.Api.Dtos;

namespace Librarium.Api.Services.Interfaces;

public interface IBookService
{
    Task<BookDto> CreateAsync(CreateBookRequest request);
    Task<List<BookDto>> GetAllAsync();
    Task<BookDto?> GetByIdAsync(int id);
    Task<BookDto?> UpdateAsync(int id, UpdateBookRequest request);
    Task<bool> DeleteAsync(int id);
}