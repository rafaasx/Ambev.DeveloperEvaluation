namespace Ambev.DeveloperEvaluation.Domain.Services.Interfaces;

public interface ICartPriceService
{
    Task<decimal> GetTotalPriceAsync(Guid cartId);
}