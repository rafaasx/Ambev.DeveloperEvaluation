using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Carts.CreateCart;

public class CreateCartProfile : Profile
{
    public CreateCartProfile()
    {
        CreateMap<Cart, CreateCartResult>();
        CreateMap<CartItem, CreateCartItemResult>();
    }
}
