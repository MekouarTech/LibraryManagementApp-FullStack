using MediatR;
using LibraryManagementApp.Application.DTOs;
using LibraryManagementApp.Domain.Repositories;

namespace LibraryManagementApp.Application.Publishers.Queries.GetPublisherById;

public class GetPublisherByIdQueryHandler : IRequestHandler<GetPublisherByIdQuery, PublisherDto?>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetPublisherByIdQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<PublisherDto?> Handle(GetPublisherByIdQuery request, CancellationToken cancellationToken)
    {
        var publisher = await _unitOfWork.Publishers.GetByIdAsync(request.Id);
        
        if (publisher == null)
        {
            return null;
        }

        return new PublisherDto
        {
            Id = publisher.Id,
            Name = publisher.Name
        };
    }
} 