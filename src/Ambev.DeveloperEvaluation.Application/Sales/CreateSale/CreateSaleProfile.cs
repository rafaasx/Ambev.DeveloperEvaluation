using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

public class CreateSaleProfile : Profile
{
    public CreateSaleProfile()
    {
        CreateMap<Sale, CreateSaleResult>();
        CreateMap<SaleItem, CreateSaleItemResult>();
        CreateMap<Sale, CreateSaleCommand>();
        CreateMap<SaleItem, CreateSaleItemCommand>();
        CreateMap<CreateSaleCommand, Sale>().ConstructUsing(command => new Sale(command.SaleNumber, command.SaleDate, command.CustomerId, command.CustomerName, command.BranchId, command.BranchName));

    }
}
