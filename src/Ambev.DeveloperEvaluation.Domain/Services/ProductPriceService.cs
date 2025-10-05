using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Services.Interfaces;

namespace Ambev.DeveloperEvaluation.Domain.Services;


public class ProductPriceService : IProductPriceService
{
    private readonly IProductRepository _productRepository;

    public ProductPriceService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<decimal> GetPriceAsync(Guid productId, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(productId, cancellationToken);
        return product == null ? throw new KeyNotFoundException($"Product with ID {productId} not found.") : product.Price;
    }
}
