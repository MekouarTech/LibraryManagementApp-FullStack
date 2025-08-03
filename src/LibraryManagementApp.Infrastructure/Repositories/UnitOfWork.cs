using LibraryManagementApp.Domain.Repositories;
using LibraryManagementApp.Infrastructure.Data;

namespace LibraryManagementApp.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly LibraryDbContext _context;
    private IBookRepository? _bookRepository;
    private IAuthorRepository? _authorRepository;
    private ICategoryRepository? _categoryRepository;
    private IPublisherRepository? _publisherRepository;

    public UnitOfWork(LibraryDbContext context)
    {
        _context = context;
    }

    public IBookRepository Books => _bookRepository ??= new BookRepository(_context);
    public IAuthorRepository Authors => _authorRepository ??= new AuthorRepository(_context);
    public ICategoryRepository Categories => _categoryRepository ??= new CategoryRepository(_context);
    public IPublisherRepository Publishers => _publisherRepository ??= new PublisherRepository(_context);

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
} 