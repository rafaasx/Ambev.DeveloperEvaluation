using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

public class SaleItem : BaseEntity
{
    public Guid SaleId { get; private set; }
    public int Quantity { get; private set; }
    public decimal UnitPrice { get; private set; }
    public decimal DiscountApplied { get; private set; }
    public decimal TotalItemAmount { get; private set; }
    public bool IsCancelled { get; private set; }

    public Guid ProductId { get; private set; }
    public string ProductName { get; private set; }

    protected SaleItem() { }

    public SaleItem(Guid productId, string productName, int quantity, decimal unitPrice, Guid saleId)
    {
        Id = Guid.NewGuid();
        ProductId = productId;
        ProductName = productName;
        Quantity = quantity;
        UnitPrice = unitPrice;
        SaleId = saleId;
        ApplyDiscountRules();
        CalculateTotal();
    }

    private void ApplyDiscountRules()
    {
        if (Quantity >= 4 && Quantity < 10)
            DiscountApplied = 0.10m;
        else if (Quantity >= 10 && Quantity <= 20)
            DiscountApplied = 0.20m;
        else if (Quantity > 20)
            throw new InvalidOperationException("It is not allowed to sell more than 20 identical items.");
        else
            DiscountApplied = 0.00m;
    }

    private void CalculateTotal()
    {
        TotalItemAmount = Quantity * UnitPrice * (1 - DiscountApplied);
    }

    public void Cancel() => IsCancelled = true;
}
