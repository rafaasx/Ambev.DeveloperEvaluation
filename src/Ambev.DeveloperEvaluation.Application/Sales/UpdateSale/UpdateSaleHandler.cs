using Ambev.DeveloperEvaluation.Application.Sales.UpdateSale.Commands;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;

public class UpdateSaleHandler(ISaleRepository _saleRepository, IMapper _mapper) : IRequestHandler<UpdateSaleCommand, UpdateSaleResult>
{
    public async Task<UpdateSaleResult> Handle(UpdateSaleCommand command, CancellationToken cancellationToken)
    {
        var sale = _mapper.Map<Sale>(command);

        var UpdatedSale = await _saleRepository.UpdateAsync(sale, cancellationToken);

        var response = _mapper.Map<UpdateSaleResult>(UpdatedSale);

        return response;
    }
}