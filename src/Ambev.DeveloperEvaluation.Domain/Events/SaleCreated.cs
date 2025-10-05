namespace Ambev.DeveloperEvaluation.Domain.Events;

public class SaleCreated : DomainEvent
{
    public Guid SaleId { get; }
    public string SaleNumber { get; }
    public Guid CustomerId { get; }
    public string CustomerName { get; }
    public SaleCreated(Guid saleId, string saleNumber, Guid customerId, string customerName)
    {
        SaleId = saleId;
        SaleNumber = saleNumber;
        CustomerId = customerId;
        CustomerName = customerName;
    }
}
