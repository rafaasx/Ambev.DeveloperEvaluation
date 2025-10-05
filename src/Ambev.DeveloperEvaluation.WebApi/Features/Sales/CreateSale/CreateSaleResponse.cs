namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;

public class CreateSaleResponse
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
    public List<CreateSaleItemResponse> Products { get; set; } = new();
}