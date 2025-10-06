using Ambev.DeveloperEvaluation.Application.Products.GetProduct;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.CreatProduct;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.GetProduct;

public class GetProductProfile : Profile
{
    public GetProductProfile()
    {
        CreateMap<Guid, GetProductCommand>()
            .ConstructUsing(id => new GetProductCommand(id));
        CreateMap<Product, GetProductResult>();
        CreateMap<GetProductResult, GetProductResponse>();
        CreateMap<GetRatingResult, CreateProductRatingRequest>();
    }
}
