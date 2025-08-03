using MediatR;

namespace LibraryManagementApp.Application.Books.Commands.DeleteBook;

public class DeleteBookCommand : IRequest<bool>
{
    public int Id { get; set; }
} 