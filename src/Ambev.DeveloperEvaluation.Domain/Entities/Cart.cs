using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

public class Cart : BaseEntity
{
    public Guid UserId { get; set; }
    public DateTime Date { get; set; }
    public decimal TotalPrice { get; set; }
    public User User { get; set; }
    private readonly List<CartItem> _products = new();
    public IReadOnlyCollection<CartItem> Products => _products.AsReadOnly();

    public Cart() { }

    public Cart(Guid userId, DateTime date)
    {
        Id = Guid.NewGuid();
        UserId = userId;
        Date = date;
        Date = DateTime.UtcNow;
    }

    public void UpdateProduct(Guid productId, int quantity, decimal unitPrice, string productTitle)
    {
        if (quantity < 1 || quantity > 20)
        {
            throw new ArgumentOutOfRangeException(nameof(quantity), "Quantity must be between 1 and 20");
        }

        var existingItem = Products.SingleOrDefault(p => p.ProductId == productId);

        if (existingItem != null)
        {
            existingItem.UpdateProductQuantity(quantity, unitPrice);
            existingItem.UpdateProductTitle(productTitle);
        }
        else
        {
            var newItem = new CartItem(productId, quantity, unitPrice, Id, productTitle);

            AddItem(newItem);
        }
        UpdateTotal();
    }

    public void AddItem(CartItem item) => _products.Add(item);
    public void ClearItems() => _products.Clear();

    public void AddItems(IEnumerable<CartItem> items) => items.ToList().ForEach(item => AddItem(item));

    private void UpdateTotal()
    {
        TotalPrice = Products.Sum(p => p.TotalPriceWithDiscount);
    }
}
