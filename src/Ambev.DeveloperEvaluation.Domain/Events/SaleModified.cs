namespace Ambev.DeveloperEvaluation.Domain.Events;

public class SaleModified : DomainEvent
{
    public Guid SaleId { get; }
    public decimal NewTotalAmount { get; }

    public SaleModified(Guid saleId, decimal newTotalAmount)
    {
        SaleId = saleId;
        NewTotalAmount = newTotalAmount;
    }
}
