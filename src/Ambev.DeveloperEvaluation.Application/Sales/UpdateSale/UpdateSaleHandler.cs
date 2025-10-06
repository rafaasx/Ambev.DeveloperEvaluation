using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;

public class UpdateSaleHandler(ISaleRepository _saleRepository, IMapper _mapper) : IRequestHandler<UpdateSaleCommand, UpdateSaleResult>
{
    public async Task<UpdateSaleResult> Handle(UpdateSaleCommand command, CancellationToken cancellationToken)
    {
        var sale = await _saleRepository.GetByIdAsync(command.Id, cancellationToken) ?? throw new KeyNotFoundException($"Sale item with ID {command.Id} not found.");
        _mapper.Map(command, sale);
        sale.UpdateItems(_mapper.Map<IEnumerable<SaleItem>>(command.Products));
        var UpdatedSale = await _saleRepository.UpdateAsync(sale, cancellationToken);

        var response = _mapper.Map<UpdateSaleResult>(UpdatedSale);

        return response;
    }
}