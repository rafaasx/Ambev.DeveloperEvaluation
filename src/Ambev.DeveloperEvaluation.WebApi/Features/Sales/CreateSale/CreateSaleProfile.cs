using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;

public class CreateSaleProfile : Profile
{

    public CreateSaleProfile()
    {
        CreateMap<CreateSaleRequest, CreateSaleCommand>();
        CreateMap<CreateSaleItemRequest, CreateSaleItemCommand>();
        CreateMap<CreateSaleResult, CreateSaleResponse>();
        CreateMap<CreateSaleItemResult, CreateSaleItemResponse>();
        CreateMap<Sale, CreateSaleResult>();
        CreateMap<Sale, CreateSaleCommand>();
        CreateMap<SaleItem, CreateSaleItemCommand>();
    }
}
