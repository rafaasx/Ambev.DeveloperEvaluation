using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale;

public class UpdateSaleRequestValidator : AbstractValidator<UpdateSaleRequest>
{
    public UpdateSaleRequestValidator()
    {
        RuleFor(Sale => Sale.Id)
            .NotEmpty().WithMessage("Sale ID is required.");

        RuleFor(Sale => Sale.CustomerId)
            .NotEmpty().WithMessage("CustomerId ID is required.");

        RuleFor(Sale => Sale.Products)
            .NotEmpty().WithMessage("Sale must contain at least one item.");

    }
}
