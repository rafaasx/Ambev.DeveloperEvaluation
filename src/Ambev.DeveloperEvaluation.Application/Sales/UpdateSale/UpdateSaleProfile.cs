using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;

public class UpdateSaleProfile : Profile
{
    public UpdateSaleProfile()
    {
        CreateMap<Sale, UpdateSaleResult>();
        CreateMap<SaleItem, UpdateSaleItemResult>();
        CreateMap<SaleItem, UpdateSaleItemCommand>().ReverseMap();

        CreateMap<Sale, UpdateSaleCommand>()
            .ForMember(dest => dest.Products, opt => opt.MapFrom(src => src.Products.ToList()))
            .ReverseMap()
            .ForMember(dest => dest.Products, opt => opt.Ignore());
    }
}
