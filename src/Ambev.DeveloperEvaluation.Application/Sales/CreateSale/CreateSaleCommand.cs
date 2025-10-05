using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale.Commands;

public class CreateSaleCommand : IRequest<CreateSaleResult>
{
    public string SaleNumber { get; set; }
    public DateTime SaleDate { get; set; }
    public decimal TotalAmount { get; set; }
    public bool IsCancelled { get; set; }
    public Guid CustomerId { get; set; }
    public string CustomerName { get; set; }
    public Guid BranchId { get; set; }
    public string BranchName { get; set; }
    public List<CreateSaleItemCommand> Products = new();

}