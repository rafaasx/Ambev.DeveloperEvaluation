namespace Ambev.DeveloperEvaluation.Application.Products.GetAllProducts;

public class GetAllProductsResult
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string Description { get; set; } = string.Empty;
    public string Image { get; set; } = string.Empty;
    public string Category { get; set; } = default!;
    public GetlAllProductsRatingResponse Rating { get; set; } = default!;
}