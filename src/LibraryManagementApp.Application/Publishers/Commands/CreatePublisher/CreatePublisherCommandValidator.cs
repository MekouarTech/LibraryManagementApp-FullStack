using FluentValidation;
using LibraryManagementApp.Application.Publishers.Commands.CreatePublisher;

namespace LibraryManagementApp.Application.Publishers.Commands.CreatePublisher;

public class CreatePublisherCommandValidator : AbstractValidator<CreatePublisherCommand>
{
    public CreatePublisherCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required")
            .MaximumLength(200).WithMessage("Name cannot exceed 200 characters");
    }
} 