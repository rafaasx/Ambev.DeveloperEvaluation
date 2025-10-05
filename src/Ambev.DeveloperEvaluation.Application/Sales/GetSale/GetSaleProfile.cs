using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSale
{
    public class GetSaleProfile : Profile
    {
        public GetSaleProfile()
        {
            CreateMap<Sale, GetSaleResult>();
            CreateMap<SaleItem, GetSaleItemResult>();
            CreateMap<Guid, GetSaleCommand>().ConstructUsing(id => new GetSaleCommand(id));
        }
    }
}
