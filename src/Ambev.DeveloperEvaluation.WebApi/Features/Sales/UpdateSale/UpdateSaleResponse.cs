namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale;

public class UpdateSaleResponse
{
    public Guid Id { get; set; }
    public string SaleNumber { get; private set; }
    public DateTime SaleDate { get; private set; }
    public decimal TotalAmount { get; private set; }
    public bool IsCancelled { get; private set; }
    public Guid CustomerId { get; private set; }
    public string CustomerName { get; private set; }
    public Guid BranchId { get; private set; }
    public string BranchName { get; private set; }
    public List<UpdateSaleItemResponse> Products { get; set; } = new();
}
