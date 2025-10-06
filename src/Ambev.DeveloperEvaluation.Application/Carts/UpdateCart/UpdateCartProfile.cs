using Ambev.DeveloperEvaluation.Application.Carts.UpdateCart.Commands;
using Ambev.DeveloperEvaluation.Application.Carts.UpdateCart.Results;
using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Carts.UpdateCart;

public class UpdateCartProfile : Profile
{
    public UpdateCartProfile()
    {
        CreateMap<Cart, UpdateCartResult>();
        CreateMap<CartItem, UpdateCartItemResult>();

        CreateMap<Cart, UpdateCartCommand>()
           .ForMember(dest => dest.Products, opt => opt.MapFrom(src => src.Products.ToList()))
           .ReverseMap()
           .ForMember(dest => dest.Products, opt => opt.Ignore());
    }
}
