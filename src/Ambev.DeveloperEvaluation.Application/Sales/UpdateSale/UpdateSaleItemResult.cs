namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;

public class UpdateSaleItemResult
{
    public Guid Id { get; set; }
    public Guid SaleId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal DiscountApplied { get; set; }
    public decimal TotalItemAmount { get; set; }
    public bool IsCancelled { get; set; }

    public Guid ProductId { get; set; }
    public string ProductName { get; set; }
}
