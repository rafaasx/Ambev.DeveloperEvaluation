using Ambev.DeveloperEvaluation.Application.Users.GetUser;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Carts.GetCart;

public class GetCartCommandValidator : AbstractValidator<GetUserCommand>
{
    public GetCartCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("User ID is required");
    }
}
