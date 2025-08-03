using MediatR;
using LibraryManagementApp.Application.DTOs;

namespace LibraryManagementApp.Application.Categories.Queries.GetAllCategories;

public class GetAllCategoriesQuery : IRequest<IEnumerable<CategoryDto>>
{
} 