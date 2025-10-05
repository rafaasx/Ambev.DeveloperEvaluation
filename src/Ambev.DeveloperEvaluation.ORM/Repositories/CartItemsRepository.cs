using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.ORM.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories;

public class CartItemsRepository : ICartItemsRepository
{
    private readonly DefaultContext _context;

    public CartItemsRepository(DefaultContext context)
    {
        _context = context;
    }

    public async Task<CartItem> CreateAsync(CartItem cartItem, CancellationToken cancellationToken = default)
    {
        await _context.CartItems.AddAsync(cartItem, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return cartItem;
    }

    public async Task<CartItem> UpdateAsync(Guid id, CartItem cartItem, CancellationToken cancellationToken = default)
    {
        var existingItem = await _context.CartItems
            .FirstOrDefaultAsync(ci => ci.Id == id, cancellationToken);

        if (existingItem == null)
            throw new KeyNotFoundException($"Cart item with ID {id} not found.");

        await _context.SaveChangesAsync(cancellationToken);
        return existingItem;
    }

    public async Task<CartItem?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.CartItems
            .FirstOrDefaultAsync(ci => ci.Id == id, cancellationToken);
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var cartItem = await GetByIdAsync(id, cancellationToken);
        if (cartItem == null)
            return false;

        _context.CartItems.Remove(cartItem);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }

    public IQueryable<CartItem> GetAllAsync(int page, int size, string order, CancellationToken cancellationToken = default)
    {
        var cartItems = _context.CartItems
            .AsQueryable()
            .SortBy(order)
            .Skip((page - 1) * size)
            .Take(size);

        return cartItems;
    }

    public async Task<CartItem> UpdateAsync(CartItem cart, CancellationToken cancellationToken = default)
    {
        var existingCartItem = await _context.CartItems
                                         .AsNoTracking()
                                         .SingleOrDefaultAsync(c => c.Id == cart.Id, cancellationToken: cancellationToken)
            ?? throw new KeyNotFoundException("Cart item not foound.");

        _context.Entry(existingCartItem).CurrentValues.SetValues(cart);
        await _context.SaveChangesAsync(cancellationToken);

        return existingCartItem;
    }
}
