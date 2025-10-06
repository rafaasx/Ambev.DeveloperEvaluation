namespace Ambev.DeveloperEvaluation.Domain.Events;

public abstract class DomainEvent : IDomainEvent
{
    public DateTime OccurredOn { get; set; } = DateTime.UtcNow;
}
