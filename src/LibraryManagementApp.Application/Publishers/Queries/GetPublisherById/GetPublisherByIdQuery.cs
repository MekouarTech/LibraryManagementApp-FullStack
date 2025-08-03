using MediatR;
using LibraryManagementApp.Application.DTOs;

namespace LibraryManagementApp.Application.Publishers.Queries.GetPublisherById;

public class GetPublisherByIdQuery : IRequest<PublisherDto?>
{
    public int Id { get; set; }
} 