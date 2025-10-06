using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

public class CreateSaleHandler(ISaleRepository _saleRepository, IMapper _mapper) : IRequestHandler<CreateSaleCommand, CreateSaleResult>
{
    public async Task<CreateSaleResult> Handle(CreateSaleCommand command, CancellationToken cancellationToken)
    {
        var sale = new Sale(command.SaleNumber, command.SaleDate, command.CustomerId, command.CustomerName, command.BranchId, command.BranchName);
        sale.AddItems(command.Products.Select(item => new SaleItem(item.ProductId, item.ProductName, item.Quantity, item.UnitPrice, sale.Id)));
        var createdSale = await _saleRepository.CreateAsync(sale, cancellationToken);

        var response = _mapper.Map<CreateSaleResult>(createdSale);

        return response;
    }
}