using LibraryManagementApp.Domain.Entities;
using MediatR;

namespace LibraryManagementApp.Domain.Events;

public class BookCreatedEvent : IDomainEvent, INotification
{
    public Book Book { get; }
    public DateTime OccurredOn { get; }

    public BookCreatedEvent(Book book)
    {
        Book = book;
        OccurredOn = DateTime.UtcNow;
    }
} 