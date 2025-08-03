using MediatR;
using LibraryManagementApp.Application.DTOs;

namespace LibraryManagementApp.Application.Categories.Commands.CreateCategory;

public class CreateCategoryCommand : IRequest<CategoryDto>
{
    public string Name { get; set; } = string.Empty;
} 