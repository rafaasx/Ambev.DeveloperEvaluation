﻿using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Products.GetProduct;

public class GetProductProfile : Profile
{

    public GetProductProfile()
    {
        CreateMap<Guid, GetProductCommand>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src));

        CreateMap<Product, GetProductResult>();

        CreateMap<Rating, GetRatingResult>();
    }
}
