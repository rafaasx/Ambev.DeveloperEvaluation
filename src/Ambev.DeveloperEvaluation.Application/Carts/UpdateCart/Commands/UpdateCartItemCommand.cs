namespace Ambev.DeveloperEvaluation.Application.Carts.UpdateCart.Commands;

public class UpdateCartItemCommand
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public string ProductTitle { get; set; }
    public int Quantity { get; set; }
}
