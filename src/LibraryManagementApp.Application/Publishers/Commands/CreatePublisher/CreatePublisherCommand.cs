using MediatR;
using LibraryManagementApp.Application.DTOs;

namespace LibraryManagementApp.Application.Publishers.Commands.CreatePublisher;

public class CreatePublisherCommand : IRequest<PublisherDto>
{
    public string Name { get; set; } = string.Empty;
} 