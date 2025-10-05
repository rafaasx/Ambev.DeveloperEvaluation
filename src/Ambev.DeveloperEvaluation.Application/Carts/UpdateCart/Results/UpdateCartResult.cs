namespace Ambev.DeveloperEvaluation.Application.Carts.UpdateCart.Results;

public class UpdateCartResult
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public DateTime Date { get; set; }
    public List<UpdateCartItemResult> Products { get; set; } = new();

}