using MediatR;
using LibraryManagementApp.Application.DTOs;

namespace LibraryManagementApp.Application.Books.Queries.GetBookById;

public class GetBookByIdQuery : IRequest<BookDto?>
{
    public int Id { get; set; }
} 