namespace Ambev.DeveloperEvaluation.Application.Sales.GetAllSales;

public class GetAllSalesResult
{
    public string SaleNumber { get; set; }
    public DateTime SaleDate { get; set; }
    public decimal TotalAmount { get; set; }
    public bool IsCancelled { get; set; }
    public Guid CustomerId { get; set; }
    public string CustomerName { get; set; }
    public Guid BranchId { get; set; }
    public string BranchName { get; set; }

    public List<GetAllSaleItemsResult> Products = new();
}
