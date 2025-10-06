namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale;

public class UpdateSaleResponse
{
    public Guid Id { get; set; }
    public string SaleNumber { get; set; }
    public DateTime SaleDate { get; set; }
    public decimal TotalAmount { get; set; }
    public bool IsCancelled { get; set; }
    public Guid CustomerId { get; set; }
    public string CustomerName { get; set; }
    public Guid BranchId { get; set; }
    public string BranchName { get; set; }
    public List<UpdateSaleItemResponse> Products { get; set; } = new();
}
