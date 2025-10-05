using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Repositories;


public interface IProductRepository
{
    Task<Product?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Product> CreateAsync(Product product, CancellationToken cancellationToken = default);
    Task<Product> UpdateAsync(Product product, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    Task<List<string>> GetCategoriesAsync(CancellationToken cancellationToken = default);
    IQueryable<Product> GetProductsByCategory(string category, int page = 1, int size = 10, string? order = null);
    IQueryable<Product> GetAllPaginated(int page = 1, int size = 10, string? order = null);
}