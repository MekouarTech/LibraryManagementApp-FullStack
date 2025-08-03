using LibraryManagementApp.Domain.Entities;

namespace LibraryManagementApp.Domain.Repositories;

public interface IBookRepository
{
    Task<IEnumerable<Book>> GetAllAsync();
    Task<Book?> GetByIdAsync(int id);
    Task<Book> AddAsync(Book book);
    Task<Book> UpdateAsync(Book book);
    Task DeleteAsync(int id);
    Task<IEnumerable<Book>> GetByAuthorAsync(int authorId);
    Task<IEnumerable<Book>> GetByCategoryAsync(int categoryId);
    Task<IEnumerable<Book>> GetByPublisherAsync(int publisherId);
} 