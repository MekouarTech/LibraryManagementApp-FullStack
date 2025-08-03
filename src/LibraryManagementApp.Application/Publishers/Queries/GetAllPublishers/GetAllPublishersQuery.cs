using MediatR;
using LibraryManagementApp.Application.DTOs;

namespace LibraryManagementApp.Application.Publishers.Queries.GetAllPublishers;

public class GetAllPublishersQuery : IRequest<IEnumerable<PublisherDto>>
{
} 