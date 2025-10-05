namespace Ambev.DeveloperEvaluation.Application.Carts.GetAllCarts;

public class GetAllCartsResult
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public DateTime Date { get; set; }
    public ICollection<GetAllCartItemsResult> Products { get; set; } = [];
}
