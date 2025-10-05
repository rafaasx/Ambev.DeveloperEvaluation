namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.GetCart;

public class GetCartItemResponse
{
    public Guid Id { get; set; }
    public Guid ProductId { get; private set; }
    public string ProductTitle { get; set; }
    public int Quantity { get; private set; }
    public decimal UnitPrice { get; private set; }
    public decimal TotalPrice { get; private set; }
    public decimal TotalPriceWithDiscount { get; private set; }
    public decimal Discount { get; private set; }
}