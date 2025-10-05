using Ambev.DeveloperEvaluation.Application.Carts.UpdateCart.Results;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Carts.UpdateCart.Commands;

public class UpdateCartCommand : IRequest<UpdateCartResult>
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public DateTime Date { get; set; }
    public List<UpdateCartItemCommand> Products { get; set; } = [];
}