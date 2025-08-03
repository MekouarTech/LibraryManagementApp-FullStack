using MediatR;
using LibraryManagementApp.Application.DTOs;
using LibraryManagementApp.Domain.Repositories;

namespace LibraryManagementApp.Application.Books.Queries.GetAllBooks;

public class GetAllBooksQueryHandler : IRequestHandler<GetAllBooksQuery, IEnumerable<BookDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAllBooksQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<BookDto>> Handle(GetAllBooksQuery request, CancellationToken cancellationToken)
    {
        var books = await _unitOfWork.Books.GetAllAsync();
        
        return books.Select(book => new BookDto
        {
            Id = book.Id,
            Title = book.Title,
            PublicationYear = book.PublicationYear,
            NumberOfCopies = book.NumberOfCopies,
            PublisherId = book.PublisherId,
            PublisherName = book.Publisher?.Name ?? string.Empty,
            Authors = book.Authors.Select(a => new AuthorDto
            {
                Id = a.Id,
                FirstName = a.FirstName,
                LastName = a.LastName,
                Biography = a.Biography,
                DateOfBirth = a.DateOfBirth
            }).ToList(),
            Categories = book.Categories.Select(c => new CategoryDto
            {
                Id = c.Id,
                Name = c.Name
            }).ToList()
        });
    }
} 