namespace Ambev.DeveloperEvaluation.Application.Carts.UpdateCart.Results;

public class UpdateCartItemResult
{
    public Guid Id { get; set; }
    public Guid ProductId { get; private set; }
    public string ProductTitle { get; set; }
    public int Quantity { get; private set; }
    public decimal UnitPrice { get; private set; }
}
