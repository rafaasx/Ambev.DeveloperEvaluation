namespace Ambev.DeveloperEvaluation.Domain.Events;

public class SaleItemCancelled : DomainEvent
{
    public Guid SaleId { get; }
    public Guid SaleItemId { get; }
    public Guid ProductId { get; }
    public string ProductName { get; }

    public SaleItemCancelled(Guid saleId, Guid saleItemId, Guid productId, string productName)
    {
        SaleId = saleId;
        SaleItemId = saleItemId;
        ProductId = productId;
        ProductName = productName;
    }
}
