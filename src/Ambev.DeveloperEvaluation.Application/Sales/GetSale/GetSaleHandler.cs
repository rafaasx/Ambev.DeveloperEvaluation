using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSale;

public class GetSaleHandler(ISaleRepository _saleRepository, IMapper _mapper) : IRequestHandler<GetSaleCommand, GetSaleResult>
{
    public async Task<GetSaleResult> Handle(GetSaleCommand request, CancellationToken cancellationToken)
    {
        var sale = await _saleRepository.GetByIdAsync(request.Id, cancellationToken);

        return sale == null
            ? throw new KeyNotFoundException($"Sale with ID {request.Id} not found")
            : _mapper.Map<GetSaleResult>(sale);
    }
}