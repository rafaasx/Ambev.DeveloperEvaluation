using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Products.GetAllProducts;

public class GetAllProductsProfile : Profile
{
    public GetAllProductsProfile()
    {
        CreateMap<Product, GetAllProductsResult>();

        CreateMap<Category, GetAllProductsCategoryResult>()
            .ForMember(dest => dest.ExternalId, opt => opt.MapFrom(src => src.ExternalId))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));

        CreateMap<Rating, GetlAllProductsRatingResponse>();
    }
}
