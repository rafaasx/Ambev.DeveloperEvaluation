using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetAllSales
{
    public class GetAllSalesProfile : Profile
    {
        public GetAllSalesProfile()
        {
            CreateMap<Sale, GetAllSalesResult>();
            CreateMap<SaleItem, GetAllSaleItemsResult>();
        }
    }
}
