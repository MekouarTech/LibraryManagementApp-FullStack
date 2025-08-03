using MediatR;
using LibraryManagementApp.Domain.Repositories;

namespace LibraryManagementApp.Application.Authors.Commands.DeleteAuthor;

public class DeleteAuthorCommandHandler : IRequestHandler<DeleteAuthorCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteAuthorCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(DeleteAuthorCommand request, CancellationToken cancellationToken)
    {
        var author = await _unitOfWork.Authors.GetByIdAsync(request.Id);
        if (author == null)
        {
            return false;
        }

        await _unitOfWork.Authors.DeleteAsync(request.Id);
        await _unitOfWork.SaveChangesAsync();
        
        return true;
    }
} 