using MediatR;
using LibraryManagementApp.Application.DTOs;
using LibraryManagementApp.Domain.Repositories;

namespace LibraryManagementApp.Application.Authors.Queries.GetAuthorById;

public class GetAuthorByIdQueryHandler : IRequestHandler<GetAuthorByIdQuery, AuthorDto?>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAuthorByIdQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<AuthorDto?> Handle(GetAuthorByIdQuery request, CancellationToken cancellationToken)
    {
        var author = await _unitOfWork.Authors.GetByIdAsync(request.Id);
        
        if (author == null)
        {
            return null;
        }

        return new AuthorDto
        {
            Id = author.Id,
            FirstName = author.FirstName,
            LastName = author.LastName,
            Biography = author.Biography,
            DateOfBirth = author.DateOfBirth
        };
    }
} 