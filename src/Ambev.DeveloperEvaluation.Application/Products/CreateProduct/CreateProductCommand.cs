using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.CreateProduct;

public class CreateProductCommand : IRequest<CreateProductResult>
{
    public CreateProductCommand()
    {
    }

    public CreateProductCommand(string title, decimal price, string description, string image, string category, CreateRatingCommand rating)
    {
        Title = title;
        Price = price;
        Description = description;
        Image = image;
        Category = category;
        Rating = rating;
    }

    public string Title { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string Description { get; set; } = string.Empty;
    public string Image { get; set; } = string.Empty;
    public string Category { get; set; } = default!;
    public CreateRatingCommand Rating { get; set; } = default!;

}