using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.ORM.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace Ambev.DeveloperEvaluation.ORM.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly DefaultContext _context;

    public ProductRepository(DefaultContext context)
    {
        _context = context;
    }

    public IQueryable<Product> GetAllPaginated(int page = 1, int size = 10, string? order = null)
    {
        var items = _context.Products.AsQueryable()
            .SortBy(order)
            .Skip((page - 1) * size)
             .Take(size);

        return items;
    }

    public async Task<Product?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Products.FirstOrDefaultAsync(p => p.Id == id, cancellationToken: cancellationToken);
    }

    public async Task<Product> CreateAsync(Product product, CancellationToken cancellationToken = default)
    {
        await _context.Products.AddAsync(product, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return product;
    }

    public async Task<Product> UpdateAsync(Product product, CancellationToken cancellationToken = default)
    {
        var existingProduct = await _context.Products.FindAsync([product.Id], cancellationToken: cancellationToken)
            ?? throw new KeyNotFoundException("Produto não encontrado.");
        _context.Entry(existingProduct).CurrentValues.SetValues(product);

        existingProduct.UpdateTitle(product.Title);
        existingProduct.UpdatePrice(product.Price);
        existingProduct.UpdateDescription(product.Description);
        existingProduct.UpdateImage(product.Image);
        existingProduct.UpdateCategory(product.Category);
        existingProduct.UpdateRating(product.Rating);

        await _context.SaveChangesAsync(cancellationToken);
        return product;
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await _context.Products.FindAsync([id], cancellationToken);
        if (entity == null)
            return false;
        var product = await _context.Products.FindAsync([id], cancellationToken: cancellationToken)
            ?? throw new KeyNotFoundException("Produto não encontrado.");
        _context.Products.Remove(product);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<List<string>> GetCategoriesAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Products
        .Select(p => p.Category)
        .Distinct()
        .ToListAsync();
    }

    public IQueryable<Product> GetProductsByCategory(string category, int page = 1, int size = 10, string? order = null)
    {
        var items = _context.Products.AsQueryable()
            .Where(p => p.Category == category)
            .SortBy(order)
            .Skip((page - 1) * size)
            .Take(size);

        return items;
    }


}
