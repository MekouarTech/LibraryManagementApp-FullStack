using MediatR;
using LibraryManagementApp.Application.DTOs;

namespace LibraryManagementApp.Application.Categories.Queries.GetCategoryById;

public class GetCategoryByIdQuery : IRequest<CategoryDto?>
{
    public int Id { get; set; }
} 