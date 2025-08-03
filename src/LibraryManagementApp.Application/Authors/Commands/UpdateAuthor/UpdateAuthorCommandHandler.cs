using MediatR;
using LibraryManagementApp.Application.DTOs;
using LibraryManagementApp.Domain.Entities;
using LibraryManagementApp.Domain.Repositories;

namespace LibraryManagementApp.Application.Authors.Commands.UpdateAuthor;

public class UpdateAuthorCommandHandler : IRequestHandler<UpdateAuthorCommand, AuthorDto>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateAuthorCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<AuthorDto> Handle(UpdateAuthorCommand request, CancellationToken cancellationToken)
    {
        var author = await _unitOfWork.Authors.GetByIdAsync(request.Id);
        if (author == null)
        {
            throw new ArgumentException($"Author with ID {request.Id} not found.");
        }

        author.FirstName = request.FirstName;
        author.LastName = request.LastName;
        author.Biography = request.Biography;
        author.DateOfBirth = request.DateOfBirth;

        var updatedAuthor = await _unitOfWork.Authors.UpdateAsync(author);
        await _unitOfWork.SaveChangesAsync();

        return new AuthorDto
        {
            Id = updatedAuthor.Id,
            FirstName = updatedAuthor.FirstName,
            LastName = updatedAuthor.LastName,
            Biography = updatedAuthor.Biography,
            DateOfBirth = updatedAuthor.DateOfBirth
        };
    }
} 