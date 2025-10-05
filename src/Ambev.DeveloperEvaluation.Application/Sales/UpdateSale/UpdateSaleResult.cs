namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;

public class UpdateSaleResult
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public DateTime Date { get; set; }
    public decimal TotalPrice { get; set; }
    public List<UpdateSaleItemResult> Products { get; set; } = new();
}