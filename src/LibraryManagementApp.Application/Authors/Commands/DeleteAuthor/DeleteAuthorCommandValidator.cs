using FluentValidation;
using LibraryManagementApp.Application.Authors.Commands.DeleteAuthor;

namespace LibraryManagementApp.Application.Authors.Commands.DeleteAuthor;

public class DeleteAuthorCommandValidator : AbstractValidator<DeleteAuthorCommand>
{
    public DeleteAuthorCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Author ID must be greater than 0");
    }
} 