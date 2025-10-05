using Ambev.DeveloperEvaluation.Application.Carts.GetAllCarts;
using Ambev.DeveloperEvaluation.WebApi.Features.Carts.CreateCart;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.GetAllCart;

public class GetAllCartsProfile : Profile
{
    public GetAllCartsProfile()
    {
        CreateMap<GetAllCartsResult, GetAllCartsResponse>();
        CreateMap<GetAllCartItemsResult, CreateCartItemResponse>();
    }
}
