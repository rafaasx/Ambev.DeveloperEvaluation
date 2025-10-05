namespace Ambev.DeveloperEvaluation.Application.Carts.CreateCart;

public class CreateCartItemResult
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public string ProductTitle { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal TotalPrice { get; set; }
    public decimal TotalPriceWithDiscount { get; set; }
    public decimal Discount { get; set; }
}
