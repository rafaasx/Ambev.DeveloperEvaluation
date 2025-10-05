using Ambev.DeveloperEvaluation.Application.Sales.GetAllSales;
using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetAllSale;

public class GetAllSalesProfile : Profile
{
    public GetAllSalesProfile()
    {
        CreateMap<GetAllSalesResult, GetAllSalesResponse>();
        CreateMap<Sale, GetAllSalesResponse>();
    }
}
