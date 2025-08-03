using MediatR;
using LibraryManagementApp.Application.DTOs;

namespace LibraryManagementApp.Application.Books.Queries.GetAllBooks;

public class GetAllBooksQuery : IRequest<IEnumerable<BookDto>>
{
} 