namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSale;

public class GetSaleResponse
{
    public Guid Id { get; set; }
    public string SaleNumber { get; set; } = default!;
    public DateTime SaleDate { get; set; }
    public decimal TotalAmount { get; set; }
    public bool IsCancelled { get; set; }
    public Guid CustomerId { get; set; }
    public string CustomerName { get; set; } = default!;
    public Guid BranchId { get; set; }
    public string BranchName { get; set; } = default!;

    public List<GetSaleItemResponse> Products = new();

}
