using LibraryManagementApp.Domain.Entities;

namespace LibraryManagementApp.Domain.Events;

public class BookCreatedEvent : IDomainEvent
{
    public Book Book { get; }
    public DateTime OccurredOn { get; }

    public BookCreatedEvent(Book book)
    {
        Book = book;
        OccurredOn = DateTime.UtcNow;
    }
} 