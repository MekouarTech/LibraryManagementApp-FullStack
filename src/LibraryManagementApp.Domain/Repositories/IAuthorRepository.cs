using LibraryManagementApp.Domain.Entities;

namespace LibraryManagementApp.Domain.Repositories;

public interface IAuthorRepository
{
    Task<IEnumerable<Author>> GetAllAsync();
    Task<Author?> GetByIdAsync(int id);
    Task<Author> AddAsync(Author author);
    Task<Author> UpdateAsync(Author author);
    Task DeleteAsync(int id);
} 