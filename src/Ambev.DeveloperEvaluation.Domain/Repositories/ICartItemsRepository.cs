using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Repositories;

public interface ICartItemsRepository
{
    Task<CartItem> CreateAsync(CartItem cartItem, CancellationToken cancellationToken = default);
    Task<CartItem> UpdateAsync(Guid id, CartItem cartItem, CancellationToken cancellationToken = default);
    Task<CartItem?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
