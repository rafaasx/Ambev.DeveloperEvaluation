using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Services.Interfaces;
using AutoMapper;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Carts.CreateCart;

public class CreateCartHandler : IRequestHandler<CreateCartCommand, CreateCartResult>
{
    private readonly ICartRepository _cartRepository;
    private readonly IProductPriceService _productPriceService;
    private readonly IMapper _mapper;
    public CreateCartHandler(ICartRepository cartRepository, IProductPriceService productPriceService, IMapper mapper)
    {
        _cartRepository = cartRepository;
        _mapper = mapper;
        _productPriceService = productPriceService;
    }

    public async Task<CreateCartResult> Handle(CreateCartCommand command, CancellationToken cancellationToken)
    {
        var cart = new Cart(command.UserId, command.Date);
        cart.AddItems(command.Products.Select(s => new CartItem(s.ProductId, s.Quantity, s.UnitPrice, cart.Id, s.ProductTitle)));

        foreach (var product in command.Products)
        {
            var unitPrice = await _productPriceService.GetPriceAsync(product.ProductId, cancellationToken);
            cart.UpdateProduct(product.ProductId, product.Quantity, unitPrice, product.ProductTitle);
        }

        var createdCart = await _cartRepository.CreateAsync(cart, cancellationToken);

        var response = _mapper.Map<CreateCartResult>(createdCart);

        return response;
    }
}