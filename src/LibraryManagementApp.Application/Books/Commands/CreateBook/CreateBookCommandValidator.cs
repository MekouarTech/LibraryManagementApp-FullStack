using FluentValidation;
using LibraryManagementApp.Application.Books.Commands.CreateBook;

namespace LibraryManagementApp.Application.Books.Commands.CreateBook;

public class CreateBookCommandValidator : AbstractValidator<CreateBookCommand>
{
    public CreateBookCommandValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required")
            .MaximumLength(200).WithMessage("Title cannot exceed 200 characters");

        RuleFor(x => x.PublicationYear)
            .GreaterThan(0).WithMessage("Publication year must be greater than 0")
            .LessThanOrEqualTo(DateTime.Now.Year).WithMessage("Publication year cannot be in the future");

        RuleFor(x => x.NumberOfCopies)
            .GreaterThanOrEqualTo(0).WithMessage("Number of copies must be non-negative");

        RuleFor(x => x.PublisherId)
            .GreaterThan(0).WithMessage("Publisher ID must be greater than 0");

        RuleFor(x => x.AuthorIds)
            .NotEmpty().WithMessage("At least one author must be specified");

        RuleFor(x => x.CategoryIds)
            .NotEmpty().WithMessage("At least one category must be specified");
    }
} 