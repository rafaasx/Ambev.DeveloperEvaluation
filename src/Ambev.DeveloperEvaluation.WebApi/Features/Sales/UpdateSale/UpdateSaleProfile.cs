using Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale;

public class UpdateSaleProfile : Profile
{
    public UpdateSaleProfile()
    {
        CreateMap<UpdateSaleResult, UpdateSaleResponse>();
        CreateMap<UpdateSaleItemResult, UpdateSaleItemResponse>();
        CreateMap<UpdateSaleRequest, UpdateSaleCommand>();
        CreateMap<SaleItem, UpdateSaleItemCommand>().ReverseMap();
        CreateMap<UpdateSaleItemRequest, UpdateSaleItemCommand>();
        CreateMap<Sale, UpdateSaleCommand>()
                   .ForMember(dest => dest.Products, opt => opt.MapFrom(src => src.Products.ToList()))
                   .ReverseMap()
                   .ForMember(dest => dest.Products, opt => opt.Ignore());
    }
}
