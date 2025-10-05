using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Repositories;

public interface ISaleRepository
{
    Task<Sale> CreateAsync(Sale Sale, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    IQueryable<Sale> GetAllAsync(int page, int size, string order, CancellationToken cancellationToken = default);
    Task<Sale?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Sale> UpdateAsync(Guid id, Sale Sale, CancellationToken cancellationToken = default);
    Task<Sale> UpdateAsync(Sale cart, CancellationToken cancellationToken = default);
}
