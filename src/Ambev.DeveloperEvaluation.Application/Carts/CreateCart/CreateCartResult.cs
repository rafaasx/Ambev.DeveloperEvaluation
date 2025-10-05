namespace Ambev.DeveloperEvaluation.Application.Carts.CreateCart;

public class CreateCartResult
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public DateTime Date { get; set; }
    public decimal TotalPrice { get; set; }
    public List<CreateCartItemResult> Products { get; set; } = new();
}