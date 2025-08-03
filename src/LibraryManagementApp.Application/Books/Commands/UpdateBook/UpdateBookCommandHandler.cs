using MediatR;
using LibraryManagementApp.Application.DTOs;
using LibraryManagementApp.Domain.Entities;
using LibraryManagementApp.Domain.Repositories;

namespace LibraryManagementApp.Application.Books.Commands.UpdateBook;

public class UpdateBookCommandHandler : IRequestHandler<UpdateBookCommand, BookDto>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateBookCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<BookDto> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
    {
        var book = await _unitOfWork.Books.GetByIdAsync(request.Id);
        if (book == null)
        {
            throw new ArgumentException($"Book with ID {request.Id} not found.");
        }

        // Update basic properties
        book.Title = request.Title;
        book.PublicationYear = request.PublicationYear;
        book.NumberOfCopies = request.NumberOfCopies;
        book.PublisherId = request.PublisherId;

        // Clear existing relationships
        book.Authors.Clear();
        book.Categories.Clear();

        // Add new authors
        foreach (var authorId in request.AuthorIds)
        {
            var author = await _unitOfWork.Authors.GetByIdAsync(authorId);
            if (author != null)
            {
                book.Authors.Add(author);
            }
        }

        // Add new categories
        foreach (var categoryId in request.CategoryIds)
        {
            var category = await _unitOfWork.Categories.GetByIdAsync(categoryId);
            if (category != null)
            {
                book.Categories.Add(category);
            }
        }

        var updatedBook = await _unitOfWork.Books.UpdateAsync(book);
        await _unitOfWork.SaveChangesAsync();

        return new BookDto
        {
            Id = updatedBook.Id,
            Title = updatedBook.Title,
            PublicationYear = updatedBook.PublicationYear,
            NumberOfCopies = updatedBook.NumberOfCopies,
            PublisherId = updatedBook.PublisherId,
            PublisherName = updatedBook.Publisher?.Name ?? string.Empty,
            Authors = updatedBook.Authors.Select(a => new AuthorDto
            {
                Id = a.Id,
                FirstName = a.FirstName,
                LastName = a.LastName,
                Biography = a.Biography,
                DateOfBirth = a.DateOfBirth
            }).ToList(),
            Categories = updatedBook.Categories.Select(c => new CategoryDto
            {
                Id = c.Id,
                Name = c.Name
            }).ToList()
        };
    }
} 