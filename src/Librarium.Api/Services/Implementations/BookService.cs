using Librarium.Api.Dtos;
using Librarium.Api.Services.Interfaces;
using Librarium.Data.Entities;
using Librarium.Data.Repositories.Interfaces;

namespace Librarium.Api.Services.Implementations;

public class BookService : IBookService
{
    private readonly IBookRepository _repository;

    public BookService(IBookRepository repository)
    {
        _repository = repository;
    }

    public async Task<BookDto> CreateAsync(CreateBookRequest request)
    {
        var book = new Book
        {
            Title = request.Title,
            IsbnText = request.ISBN,
            PublicationYear = request.PublicationYear
        };

        var created = await _repository.CreateAsync(book);

        return MapToDto(created);
    }

    public async Task<List<BookDto>> GetAllAsync()
    {
        var books = await _repository.GetAllAsync();
        return books
            .Where(b=>!b.IsRetired)
            .Select(MapToDto)
            .ToList();
    }

    public async Task<BookDto?> GetByIdAsync(int id)
    {
        var book = await _repository.GetByIdAsync(id);
        return book == null ? null : MapToDto(book);
    }

    public async Task<BookDto?> UpdateAsync(int id, UpdateBookRequest request)
    {
        var book = await _repository.GetByIdAsync(id);
        if (book == null) return null;

        book.Title = request.Title;
        book.IsbnText = request.ISBN;
        book.PublicationYear = request.PublicationYear;

        var updated = await _repository.UpdateAsync(book);
        return MapToDto(updated);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var book = await _repository.GetByIdAsync(id);
        if (book == null) return false;

        await _repository.DeleteAsync(book);
        return true;
    }

    private static BookDto MapToDto(Book book)
    {
        return new BookDto
        {
            BookId = book.BookId,
            Title = book.Title,
            ISBN = book.IsbnText,
            PublicationYear = book.PublicationYear,
            Authors = book.Authors.Select(a =>
                new AuthorDto(a.AuthorId, a.FirstName, a.LastName, a.Biography)
            ).ToList()
        };
    }
}