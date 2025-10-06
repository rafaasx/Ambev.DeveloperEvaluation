namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.GetCart;

public class GetCartItemResponse
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