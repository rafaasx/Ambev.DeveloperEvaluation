using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

public class CartItem : BaseEntity
{
    public Guid CartId { get; set; }
    public Guid ProductId { get; set; }
    public string ProductTitle { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal TotalPrice { get; set; }
    public decimal TotalPriceWithDiscount { get; set; }
    public decimal Discount { get; set; }

    private CartItem() { }

    public CartItem(Guid productId, int quantity, decimal unitPrice, Guid cartId, string productTitle)
    {
        Id = Guid.NewGuid();
        ProductId = productId;
        UnitPrice = unitPrice;
        UpdateProductQuantity(quantity, unitPrice);
        CartId = cartId;
        ProductTitle = productTitle;
    }

    public void UpdateProductQuantity(int quantity, decimal unitPrice)
    {
        if (quantity < 1 || 20 < quantity)
            throw new ArgumentOutOfRangeException(nameof(quantity), "Quantity must be between 1 and 20");

        Quantity = quantity;
        UnitPrice = unitPrice;
        CalculateDiscountedPrice(Quantity, unitPrice);
    }

    private void CalculateDiscountedPrice(int quantity, decimal unitPrice)
    {
        Discount = 0m;

        if (quantity >= 4 && quantity < 10)
        {
            Discount = 0.10m;
        }
        else if (quantity >= 10 && quantity <= 20)
        {
            Discount = 0.20m;
        }

        TotalPrice = unitPrice * quantity;
        TotalPriceWithDiscount = TotalPrice - (TotalPrice * Discount);
    }

    public void UpdateProductTitle(string productTitle) => ProductTitle = productTitle;
}
