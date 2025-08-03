using FluentValidation;
using LibraryManagementApp.Application.Publishers.Commands.UpdatePublisher;

namespace LibraryManagementApp.Application.Publishers.Commands.UpdatePublisher;

public class UpdatePublisherCommandValidator : AbstractValidator<UpdatePublisherCommand>
{
    public UpdatePublisherCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Publisher ID must be greater than 0");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required")
            .MaximumLength(200).WithMessage("Name cannot exceed 200 characters");
    }
} 