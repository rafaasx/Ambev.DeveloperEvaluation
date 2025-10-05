using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.ORM.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories;

public class SaleRepository : ISaleRepository
{
    private readonly DefaultContext _context;
    public SaleRepository(DefaultContext context)
    {
        _context = context;
    }

    public async Task<Sale> CreateAsync(Sale Sale, CancellationToken cancellationToken = default)
    {
        await _context.Sales.AddAsync(Sale, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return Sale;
    }

    public async Task<Sale> UpdateAsync(Guid id, Sale Sale, CancellationToken cancellationToken = default)
    {
        var existingItem = await _context.Sales
            .FirstOrDefaultAsync(ci => ci.Id == id, cancellationToken);

        if (existingItem == null)
            throw new KeyNotFoundException($"Sale item with ID {id} not found.");

        await _context.SaveChangesAsync(cancellationToken);
        return existingItem;
    }

    public async Task<Sale?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Sales
            .Include(s => s.Products)
            .FirstOrDefaultAsync(ci => ci.Id == id, cancellationToken);
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var Sale = await GetByIdAsync(id, cancellationToken);
        if (Sale == null)
            return false;

        _context.Sales.Remove(Sale);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }

    public IQueryable<Sale> GetAllAsync(int page, int size, string order, CancellationToken cancellationToken = default)
    {
        var Sales = _context.Sales
            .Include(i => i.Products)
            .AsQueryable()
            .SortBy(order)
            .Skip((page - 1) * size)
            .Take(size);

        return Sales;
    }

    public async Task<Sale> UpdateAsync(Sale sale, CancellationToken cancellationToken = default)
    {
        var existingSale = await _context.Sales
                                         .AsNoTracking()
                                         .SingleOrDefaultAsync(c => c.Id == sale.Id, cancellationToken: cancellationToken)
            ?? throw new KeyNotFoundException("Sale not foound.");

        _context.Entry(existingSale).CurrentValues.SetValues(sale);
        await _context.SaveChangesAsync(cancellationToken);

        return existingSale;
    }
}
