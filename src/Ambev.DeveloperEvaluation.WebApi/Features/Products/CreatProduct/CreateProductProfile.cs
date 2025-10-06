using Ambev.DeveloperEvaluation.Application.Products.CreateProduct;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.CreatProduct;

public class CreateProductProfile : Profile
{
    public CreateProductProfile()
    {
        CreateMap<CreateProductRequest, CreateProductCommand>().ReverseMap();
        CreateMap<CreateProductResult, CreateProductResponse>();
        CreateMap<CreateProductRatingResult, CreateProductRatingResponse>().ReverseMap();
        CreateMap<CreateProductRatingRequest, CreateRatingCommand>().ReverseMap();
    }
}

