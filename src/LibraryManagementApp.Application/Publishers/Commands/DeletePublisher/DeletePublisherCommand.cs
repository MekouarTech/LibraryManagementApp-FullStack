using MediatR;

namespace LibraryManagementApp.Application.Publishers.Commands.DeletePublisher;

public class DeletePublisherCommand : IRequest<bool>
{
    public int Id { get; set; }
} 