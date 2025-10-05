using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;

public class CreateSaleRequestValidator : AbstractValidator<CreateSaleRequest>
{

    public CreateSaleRequestValidator()
    {
        RuleFor(x => x.Products)
             .NotEmpty().WithMessage("Products list cannot be empty.")
             .Must(p => p != null && p.Count > 0).WithMessage("Products list must contain at least one item.")
             .ForEach(product =>
             {
                 product.NotNull().WithMessage("Product cannot be null.");
                 product.SetValidator(new CreateSaleItemRequestValidator());
             });
    }
}


public class CreateSaleItemRequestValidator : AbstractValidator<CreateSaleItemRequest>
{

    public CreateSaleItemRequestValidator()
    {
        RuleFor(item => item.ProductId)
            .NotEmpty().WithMessage("Product ID is required.");

        RuleFor(item => item.Quantity)
            .GreaterThan(0).WithMessage("Quantity must be greater than zero.");
    }
}
