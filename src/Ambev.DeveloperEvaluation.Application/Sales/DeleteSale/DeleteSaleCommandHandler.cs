using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.DeleteSale;

public class DeleteSaleCommandHandler(ISaleRepository _saleRepository) : IRequestHandler<DeleteSaleCommand, DeleteSaleResponse>
{
    public async Task<DeleteSaleResponse> Handle(DeleteSaleCommand request, CancellationToken cancellationToken)
    {
        var success = await _saleRepository.DeleteAsync(request.Id, cancellationToken);

        if (!success)
        {
            throw new KeyNotFoundException($"Sale with ID {request.Id} not found");
        }

        return new DeleteSaleResponse { Success = true };
    }
}
