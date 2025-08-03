using MediatR;
using LibraryManagementApp.Domain.Repositories;

namespace LibraryManagementApp.Application.Categories.Commands.DeleteCategory;

public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteCategoryCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await _unitOfWork.Categories.GetByIdAsync(request.Id);
        if (category == null)
        {
            return false;
        }

        await _unitOfWork.Categories.DeleteAsync(request.Id);
        await _unitOfWork.SaveChangesAsync();
        
        return true;
    }
} 