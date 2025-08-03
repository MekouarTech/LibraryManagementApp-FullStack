using FluentValidation;
using LibraryManagementApp.Application.Books.Commands.DeleteBook;

namespace LibraryManagementApp.Application.Books.Commands.DeleteBook;

public class DeleteBookCommandValidator : AbstractValidator<DeleteBookCommand>
{
    public DeleteBookCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Book ID must be greater than 0");
    }
} 