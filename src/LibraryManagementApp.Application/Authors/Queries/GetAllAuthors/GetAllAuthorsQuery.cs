using MediatR;
using LibraryManagementApp.Application.DTOs;

namespace LibraryManagementApp.Application.Authors.Queries.GetAllAuthors;

public class GetAllAuthorsQuery : IRequest<IEnumerable<AuthorDto>>
{
} 