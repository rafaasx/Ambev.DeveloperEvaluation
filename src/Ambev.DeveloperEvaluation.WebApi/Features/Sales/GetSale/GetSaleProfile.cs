using Ambev.DeveloperEvaluation.Application.Sales.GetSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSale;

public class GetSaleProfile : Profile
{
    public GetSaleProfile()
    {
        CreateMap<Sale, GetSaleResult>();
        CreateMap<SaleItem, GetSaleItemResult>();
        CreateMap<GetSaleItemResult, SaleItem>();

        CreateMap<GetSaleResponse, GetSaleResult>();
        CreateMap<GetSaleItemResponse, GetSaleItemResult>();

        CreateMap<GetSaleResult, GetSaleResponse>();
        CreateMap<GetSaleItemResult, GetSaleItemResponse>();

        CreateMap<Guid, GetSaleCommand>().ConstructUsing(id => new GetSaleCommand(id));
    }
}