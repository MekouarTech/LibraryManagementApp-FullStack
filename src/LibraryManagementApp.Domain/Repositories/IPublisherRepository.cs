using LibraryManagementApp.Domain.Entities;

namespace LibraryManagementApp.Domain.Repositories;

public interface IPublisherRepository
{
    Task<IEnumerable<Publisher>> GetAllAsync();
    Task<Publisher?> GetByIdAsync(int id);
    Task<Publisher> AddAsync(Publisher publisher);
    Task<Publisher> UpdateAsync(Publisher publisher);
    Task DeleteAsync(int id);
} 