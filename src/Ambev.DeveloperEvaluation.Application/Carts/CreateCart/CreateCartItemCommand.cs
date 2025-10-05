namespace Ambev.DeveloperEvaluation.Application.Carts.CreateCart;

public class CreateCartItemCommand
{
    public Guid ProductId { get; set; }
    public string ProductTitle { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
}