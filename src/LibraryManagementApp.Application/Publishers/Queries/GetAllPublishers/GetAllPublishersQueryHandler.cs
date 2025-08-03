using MediatR;
using LibraryManagementApp.Application.DTOs;
using LibraryManagementApp.Domain.Repositories;

namespace LibraryManagementApp.Application.Publishers.Queries.GetAllPublishers;

public class GetAllPublishersQueryHandler : IRequestHandler<GetAllPublishersQuery, IEnumerable<PublisherDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAllPublishersQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<PublisherDto>> Handle(GetAllPublishersQuery request, CancellationToken cancellationToken)
    {
        var publishers = await _unitOfWork.Publishers.GetAllAsync();
        
        return publishers.Select(publisher => new PublisherDto
        {
            Id = publisher.Id,
            Name = publisher.Name
        });
    }
} 