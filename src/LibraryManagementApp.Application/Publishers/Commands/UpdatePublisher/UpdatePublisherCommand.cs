using MediatR;
using LibraryManagementApp.Application.DTOs;

namespace LibraryManagementApp.Application.Publishers.Commands.UpdatePublisher;

public class UpdatePublisherCommand : IRequest<PublisherDto>
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
} 