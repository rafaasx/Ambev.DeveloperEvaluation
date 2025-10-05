namespace Ambev.DeveloperEvaluation.Domain.Events;

public interface IEventPublisher
{
    Task PublishAsync(IDomainEvent domainEvent);
}
