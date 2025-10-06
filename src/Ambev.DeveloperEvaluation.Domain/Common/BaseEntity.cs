using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Events;

namespace Ambev.DeveloperEvaluation.Domain.Common;

public class BaseEntity : IComparable<BaseEntity>
{
    public Guid Id { get; set; } = new Guid();

    protected readonly List<IDomainEvent> _domainEvents = new();
    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    public void AddEvent(IDomainEvent domainEvent) => _domainEvents.Add(domainEvent);
    public Task<IEnumerable<ValidationErrorDetail>> ValidateAsync()
    {
        return Validator.ValidateAsync(this);
    }

    public int CompareTo(BaseEntity? other)
    {
        if (other == null)
        {
            return 1;
        }

        return other!.Id.CompareTo(Id);
    }
}
