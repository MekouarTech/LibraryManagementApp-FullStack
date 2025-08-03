using Microsoft.EntityFrameworkCore;
using LibraryManagementApp.Domain.Entities;
using LibraryManagementApp.Domain.Repositories;
using LibraryManagementApp.Infrastructure.Data;

namespace LibraryManagementApp.Infrastructure.Repositories;

public class PublisherRepository : IPublisherRepository
{
    private readonly LibraryDbContext _context;

    public PublisherRepository(LibraryDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Publisher>> GetAllAsync()
    {
        return await _context.Publishers.ToListAsync();
    }

    public async Task<Publisher?> GetByIdAsync(int id)
    {
        return await _context.Publishers.FindAsync(id);
    }

    public async Task<Publisher> AddAsync(Publisher publisher)
    {
        _context.Publishers.Add(publisher);
        return publisher;
    }

    public async Task<Publisher> UpdateAsync(Publisher publisher)
    {
        _context.Publishers.Update(publisher);
        return publisher;
    }

    public async Task DeleteAsync(int id)
    {
        var publisher = await _context.Publishers.FindAsync(id);
        if (publisher != null)
        {
            _context.Publishers.Remove(publisher);
        }
    }
} 