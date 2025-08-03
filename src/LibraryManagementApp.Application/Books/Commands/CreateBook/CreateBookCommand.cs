using MediatR;
using LibraryManagementApp.Application.DTOs;

namespace LibraryManagementApp.Application.Books.Commands.CreateBook;

public class CreateBookCommand : IRequest<BookDto>
{
    public string Title { get; set; } = string.Empty;
    public int PublicationYear { get; set; }
    public int NumberOfCopies { get; set; }
    public int PublisherId { get; set; }
    public List<int> AuthorIds { get; set; } = new();
    public List<int> CategoryIds { get; set; } = new();
} 