namespace Ambev.DeveloperEvaluation.Domain.Events;

public class SaleCancelled : DomainEvent
{
    public Guid SaleId { get; }
    public string Reason { get; }

    public SaleCancelled(Guid saleId, string reason)
    {
        SaleId = saleId;
        Reason = reason;
    }
}
