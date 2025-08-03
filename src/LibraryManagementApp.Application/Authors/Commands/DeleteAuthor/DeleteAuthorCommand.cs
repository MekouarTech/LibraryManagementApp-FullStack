using MediatR;

namespace LibraryManagementApp.Application.Authors.Commands.DeleteAuthor;

public class DeleteAuthorCommand : IRequest<bool>
{
    public int Id { get; set; }
} 