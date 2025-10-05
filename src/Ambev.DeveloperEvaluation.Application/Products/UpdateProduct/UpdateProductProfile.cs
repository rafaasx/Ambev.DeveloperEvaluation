using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Products.UpdateProduct;

public class UpdateProductProfile : Profile
{
    public UpdateProductProfile()
    {
        CreateMap<UpdateProductCommand, Product>();
        CreateMap<UpdateRatingInfoCommand, Rating>();
        CreateMap<UpdateCategoryCommand, Category>();
        CreateMap<Product, UpdateProductResult>();
        CreateMap<Rating, UpdateProductRatingResult>();
    }
}
