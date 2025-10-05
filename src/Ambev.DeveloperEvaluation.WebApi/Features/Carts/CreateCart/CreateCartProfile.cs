using Ambev.DeveloperEvaluation.Application.Carts.CreateCart;
using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.CreateCart;

public class CreateCartProfile : Profile
{

    public CreateCartProfile()
    {
        CreateMap<CreateCartRequest, CreateCartCommand>().ReverseMap();
        CreateMap<CreateCartItemRequest, CreateCartItemCommand>().ReverseMap();
        CreateMap<CreateCartResult, CreateCartResponse>();
        CreateMap<CreateCartItemResult, CreateCartItemResponse>();
        CreateMap<Cart, CreateCartResult>();
    }
}
