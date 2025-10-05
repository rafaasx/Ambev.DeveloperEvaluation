﻿namespace Ambev.DeveloperEvaluation.Application.Products.GetProduct;


public class GetProductResult
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string Description { get; set; } = string.Empty;
    public string Image { get; set; } = string.Empty;
    public string Category { get; set; } = default!;
    public GetRatingResult Rating { get; set; } = default!;
}
