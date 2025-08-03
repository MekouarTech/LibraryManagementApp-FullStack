using MediatR;
using LibraryManagementApp.Domain.Repositories;

namespace LibraryManagementApp.Application.Books.Commands.DeleteBook;

public class DeleteBookCommandHandler : IRequestHandler<DeleteBookCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteBookCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
    {
        var book = await _unitOfWork.Books.GetByIdAsync(request.Id);
        if (book == null)
        {
            return false;
        }

        await _unitOfWork.Books.DeleteAsync(request.Id);
        await _unitOfWork.SaveChangesAsync();
        
        return true;
    }
} 