﻿namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;

public class UpdateSaleItemResult
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal TotalPrice { get; set; }
    public decimal TotalPriceWithDiscount { get; set; }
    public decimal Discount { get; set; }
}
