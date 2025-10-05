using Ambev.DeveloperEvaluation.Application.Carts.UpdateCart.Commands;
using Ambev.DeveloperEvaluation.Application.Carts.UpdateCart.Results;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Services.Interfaces;
using AutoMapper;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Carts.UpdateCart;

public class UpdateCartHandler : IRequestHandler<UpdateCartCommand, UpdateCartResult>
{
    private readonly ICartRepository _cartRepository;
    private readonly IProductPriceService _productPriceService;
    private readonly IMapper _mapper;

    public UpdateCartHandler(ICartRepository cartRepository, IProductPriceService productPriceService, IMapper mapper)
    {
        _cartRepository = cartRepository;
        _productPriceService = productPriceService;
        _mapper = mapper;
    }

    public async Task<UpdateCartResult> Handle(UpdateCartCommand command, CancellationToken cancellationToken)
    {
        var cart = await _cartRepository.GetByIdAsync(command.Id, cancellationToken)
            ?? throw new KeyNotFoundException($"Cart with ID {command.Id} not found.");

        foreach (var product in command.Products)
        {
            var unitPrice = await _productPriceService.GetPriceAsync(product.ProductId, cancellationToken);
            cart.UpdateProductQuantity(product.ProductId, product.Quantity, unitPrice, product.ProductTitle);
        }

        var updatedCart = await _cartRepository.UpdateAsync(cart, cancellationToken);

        return _mapper.Map<UpdateCartResult>(updatedCart);
    }
}