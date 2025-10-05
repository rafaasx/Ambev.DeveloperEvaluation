using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Products.CreateProduct;

public class CreateProductProfile : Profile
{
    public CreateProductProfile()
    {
        CreateMap<CreateProductCommand, Product>();
        CreateMap<CreateRatingCommand, Rating>();
        CreateMap<Product, CreateProductResult>();
        CreateMap<Rating, CreateProductRatingResult>();

    }
}
