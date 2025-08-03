using MediatR;
using LibraryManagementApp.Application.DTOs;
using LibraryManagementApp.Domain.Entities;
using LibraryManagementApp.Domain.Repositories;

namespace LibraryManagementApp.Application.Publishers.Commands.CreatePublisher;

public class CreatePublisherCommandHandler : IRequestHandler<CreatePublisherCommand, PublisherDto>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreatePublisherCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<PublisherDto> Handle(CreatePublisherCommand request, CancellationToken cancellationToken)
    {
        var publisher = new Publisher
        {
            Name = request.Name
        };

        var createdPublisher = await _unitOfWork.Publishers.AddAsync(publisher);
        await _unitOfWork.SaveChangesAsync();

        return new PublisherDto
        {
            Id = createdPublisher.Id,
            Name = createdPublisher.Name
        };
    }
} 