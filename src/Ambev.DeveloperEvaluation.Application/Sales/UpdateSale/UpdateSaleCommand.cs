using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale.Commands;

public record UpdateSaleCommand(
   Guid Id,
    string SaleNumber,
    DateTime SaleDate,
    Guid CustomerId,
    string CustomerName,
    Guid BranchId,
    string BranchName,
    List<UpdateSaleItemCommand> Products
) : IRequest<UpdateSaleResult>;
