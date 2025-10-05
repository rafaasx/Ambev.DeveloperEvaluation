using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

public class Product : BaseEntity
{
    public string Title { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string Description { get; set; } = string.Empty;
    public string Image { get; set; } = string.Empty;
    public string Category { get; set; } = default!;
    public Rating Rating { get; set; } = default!;

    protected Product() { }

    public Product(string title, decimal price, string description, string image, string category, Rating rating)
    {
        Id = Guid.NewGuid();
        Title = title;
        Price = price;
        Description = description;
        Image = image;
        Category = category;
        Rating = rating;
    }

    public void UpdatePrice(decimal newPrice)
    {
        if (newPrice < 0)
            throw new ArgumentOutOfRangeException(nameof(newPrice), "Price must be a positive value.");

        Price = newPrice;
    }

    public void UpdateCategory(string category) => Category = category;
    public void UpdateRating(Rating rating) => Rating = rating;
    public void UpdateTitle(string title) => Title = title;
    public void UpdateDescription(string description) => Description = description;
    public void UpdateImage(string image) => Image = image;
}
