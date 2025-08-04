using MediatR;
using Microsoft.Extensions.Logging;
using LibraryManagementApp.Domain.Events;

namespace LibraryManagementApp.Application.Books.Events;

public class BookCreatedEventHandler : INotificationHandler<BookCreatedEvent>
{
    private readonly ILogger<BookCreatedEventHandler> _logger;

    public BookCreatedEventHandler(ILogger<BookCreatedEventHandler> logger)
    {
        _logger = logger;
    }

    public async Task Handle(BookCreatedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Book created: {BookTitle} (ID: {BookId}) at {OccurredOn}", 
            notification.Book.Title, 
            notification.Book.Id, 
            notification.OccurredOn);
        
        await Task.CompletedTask;
    }
} 