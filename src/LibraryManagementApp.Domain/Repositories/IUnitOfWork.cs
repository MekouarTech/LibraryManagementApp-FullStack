namespace LibraryManagementApp.Domain.Repositories;

public interface IUnitOfWork : IDisposable
{
    IBookRepository Books { get; }
    IAuthorRepository Authors { get; }
    ICategoryRepository Categories { get; }
    IPublisherRepository Publishers { get; }
    Task<int> SaveChangesAsync();
} 