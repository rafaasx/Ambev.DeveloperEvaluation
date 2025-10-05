namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale;

public class UpdateSaleItemResponse
{
    public Guid Id { get; set; }
    public Guid SaleId { get; private set; }
    public int Quantity { get; private set; }
    public decimal UnitPrice { get; private set; }
    public decimal DiscountApplied { get; private set; }
    public decimal TotalItemAmount { get; private set; }
    public bool IsCancelled { get; private set; }
    public Guid ProductId { get; private set; }
    public string ProductName { get; private set; }
}
