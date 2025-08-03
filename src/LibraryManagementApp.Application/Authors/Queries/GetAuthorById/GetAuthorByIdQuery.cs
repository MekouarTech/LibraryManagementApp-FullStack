using MediatR;
using LibraryManagementApp.Application.DTOs;

namespace LibraryManagementApp.Application.Authors.Queries.GetAuthorById;

public class GetAuthorByIdQuery : IRequest<AuthorDto?>
{
    public int Id { get; set; }
} 