namespace LibraryManagementApp.Domain.Events;

public interface IDomainEvent
{
    DateTime OccurredOn { get; }
} 