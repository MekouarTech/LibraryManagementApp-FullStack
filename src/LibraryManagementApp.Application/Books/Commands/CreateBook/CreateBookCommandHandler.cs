using MediatR;
using LibraryManagementApp.Application.DTOs;
using LibraryManagementApp.Domain.Entities;
using LibraryManagementApp.Domain.Repositories;
using LibraryManagementApp.Domain.Events;

namespace LibraryManagementApp.Application.Books.Commands.CreateBook;

public class CreateBookCommandHandler : IRequestHandler<CreateBookCommand, BookDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPublisher _mediator;

    public CreateBookCommandHandler(IUnitOfWork unitOfWork, IPublisher mediator)
    {
        _unitOfWork = unitOfWork;
        _mediator = mediator;
    }

    public async Task<BookDto> Handle(CreateBookCommand request, CancellationToken cancellationToken)
    {
        var book = new Book
        {
            Title = request.Title,
            PublicationYear = request.PublicationYear,
            NumberOfCopies = request.NumberOfCopies,
            PublisherId = request.PublisherId
        };

        // Add authors
        foreach (var authorId in request.AuthorIds)
        {
            var author = await _unitOfWork.Authors.GetByIdAsync(authorId);
            if (author != null)
            {
                book.Authors.Add(author);
            }
        }

        // Add categories
        foreach (var categoryId in request.CategoryIds)
        {
            var category = await _unitOfWork.Categories.GetByIdAsync(categoryId);
            if (category != null)
            {
                book.Categories.Add(category);
            }
        }

        var createdBook = await _unitOfWork.Books.AddAsync(book);
        await _unitOfWork.SaveChangesAsync();

        // Publish domain event
        await _mediator.Publish(new BookCreatedEvent(createdBook), cancellationToken);

        return new BookDto
        {
            Id = createdBook.Id,
            Title = createdBook.Title,
            PublicationYear = createdBook.PublicationYear,
            NumberOfCopies = createdBook.NumberOfCopies,
            PublisherId = createdBook.PublisherId,
            PublisherName = createdBook.Publisher?.Name ?? string.Empty,
            Authors = createdBook.Authors.Select(a => new AuthorDto
            {
                Id = a.Id,
                FirstName = a.FirstName,
                LastName = a.LastName,
                Biography = a.Biography,
                DateOfBirth = a.DateOfBirth
            }).ToList(),
            Categories = createdBook.Categories.Select(c => new CategoryDto
            {
                Id = c.Id,
                Name = c.Name
            }).ToList()
        };
    }
} 