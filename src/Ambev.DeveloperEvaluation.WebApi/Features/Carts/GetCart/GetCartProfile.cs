using Ambev.DeveloperEvaluation.Application.Carts.GetCart;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.WebApi.Features.Carts.CreateCart;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.GetCart;

public class GetCartProfile : Profile
{
    public GetCartProfile()
    {
        CreateMap<Guid, GetCartCommand>()
            .ConstructUsing(id => new GetCartCommand(id));
        CreateMap<GetCartResult, GetCartResponse>();
        CreateMap<GetCartItemResult, CreateCartItemResponse>();
        CreateMap<Cart, GetCartResponse>();
        CreateMap<Cart, GetCartResult>();

        CreateMap<CartItem, CreateCartItemResponse>();
        CreateMap<CartItem, GetCartItemResult>();
        CreateMap<CartItem, GetCartItemResponse>();

    }
}
