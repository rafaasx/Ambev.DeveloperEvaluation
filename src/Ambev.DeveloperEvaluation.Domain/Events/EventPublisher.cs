using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Domain.Events;

public class EventPublisher(ILogger<EventPublisher> logger) : IEventPublisher
{
    public Task PublishAsync(IDomainEvent domainEvent)
    {
        logger.LogInformation("Domain Event Published: {EventType} - {@EventData}",
            domainEvent.GetType().Name, domainEvent);

        return Task.CompletedTask;
    }
}
