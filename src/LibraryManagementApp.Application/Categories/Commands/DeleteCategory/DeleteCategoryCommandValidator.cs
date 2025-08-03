using FluentValidation;
using LibraryManagementApp.Application.Categories.Commands.DeleteCategory;

namespace LibraryManagementApp.Application.Categories.Commands.DeleteCategory;

public class DeleteCategoryCommandValidator : AbstractValidator<DeleteCategoryCommand>
{
    public DeleteCategoryCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Category ID must be greater than 0");
    }
} 