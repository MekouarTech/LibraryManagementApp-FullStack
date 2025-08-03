using MediatR;

namespace LibraryManagementApp.Application.Categories.Commands.DeleteCategory;

public class DeleteCategoryCommand : IRequest<bool>
{
    public int Id { get; set; }
} 