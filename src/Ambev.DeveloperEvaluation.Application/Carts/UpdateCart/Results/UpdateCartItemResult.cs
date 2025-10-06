namespace Ambev.DeveloperEvaluation.Application.Carts.UpdateCart.Results;

public class UpdateCartItemResult
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public string ProductTitle { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
}
