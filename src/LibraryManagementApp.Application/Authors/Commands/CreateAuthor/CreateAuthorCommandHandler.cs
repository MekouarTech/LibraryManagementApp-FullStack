using MediatR;
using LibraryManagementApp.Application.DTOs;
using LibraryManagementApp.Domain.Entities;
using LibraryManagementApp.Domain.Repositories;

namespace LibraryManagementApp.Application.Authors.Commands.CreateAuthor;

public class CreateAuthorCommandHandler : IRequestHandler<CreateAuthorCommand, AuthorDto>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateAuthorCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<AuthorDto> Handle(CreateAuthorCommand request, CancellationToken cancellationToken)
    {
        var author = new Author
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Biography = request.Biography,
            DateOfBirth = request.DateOfBirth
        };

        var createdAuthor = await _unitOfWork.Authors.AddAsync(author);
        await _unitOfWork.SaveChangesAsync();

        return new AuthorDto
        {
            Id = createdAuthor.Id,
            FirstName = createdAuthor.FirstName,
            LastName = createdAuthor.LastName,
            Biography = createdAuthor.Biography,
            DateOfBirth = createdAuthor.DateOfBirth
        };
    }
} 