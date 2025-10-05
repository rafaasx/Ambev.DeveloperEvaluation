namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;

public record UpdateSaleItemCommand(
    Guid ProductId,
    string ProductName,
    int Quantity,
    decimal UnitPrice
);
