using MediatR;
using LibraryManagementApp.Domain.Repositories;

namespace LibraryManagementApp.Application.Publishers.Commands.DeletePublisher;

public class DeletePublisherCommandHandler : IRequestHandler<DeletePublisherCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeletePublisherCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(DeletePublisherCommand request, CancellationToken cancellationToken)
    {
        var publisher = await _unitOfWork.Publishers.GetByIdAsync(request.Id);
        if (publisher == null)
        {
            return false;
        }

        await _unitOfWork.Publishers.DeleteAsync(request.Id);
        await _unitOfWork.SaveChangesAsync();
        
        return true;
    }
} 