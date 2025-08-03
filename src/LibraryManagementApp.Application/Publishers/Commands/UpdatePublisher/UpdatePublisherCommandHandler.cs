using MediatR;
using LibraryManagementApp.Application.DTOs;
using LibraryManagementApp.Domain.Entities;
using LibraryManagementApp.Domain.Repositories;

namespace LibraryManagementApp.Application.Publishers.Commands.UpdatePublisher;

public class UpdatePublisherCommandHandler : IRequestHandler<UpdatePublisherCommand, PublisherDto>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdatePublisherCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<PublisherDto> Handle(UpdatePublisherCommand request, CancellationToken cancellationToken)
    {
        var publisher = await _unitOfWork.Publishers.GetByIdAsync(request.Id);
        if (publisher == null)
        {
            throw new ArgumentException($"Publisher with ID {request.Id} not found.");
        }

        publisher.Name = request.Name;

        var updatedPublisher = await _unitOfWork.Publishers.UpdateAsync(publisher);
        await _unitOfWork.SaveChangesAsync();

        return new PublisherDto
        {
            Id = updatedPublisher.Id,
            Name = updatedPublisher.Name
        };
    }
} 