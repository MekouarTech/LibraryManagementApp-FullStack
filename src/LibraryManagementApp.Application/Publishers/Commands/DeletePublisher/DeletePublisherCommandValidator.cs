using FluentValidation;
using LibraryManagementApp.Application.Publishers.Commands.DeletePublisher;

namespace LibraryManagementApp.Application.Publishers.Commands.DeletePublisher;

public class DeletePublisherCommandValidator : AbstractValidator<DeletePublisherCommand>
{
    public DeletePublisherCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Publisher ID must be greater than 0");
    }
} 