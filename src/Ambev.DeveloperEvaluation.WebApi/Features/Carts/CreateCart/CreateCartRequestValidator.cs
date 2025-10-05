using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.CreateCart;

public class CreateCartRequestValidator : AbstractValidator<CreateCartRequest>
{

    public CreateCartRequestValidator()
    {
        RuleFor(x => x.UserId)
             .NotEmpty().WithMessage("UserId is required.")
             .NotEqual(Guid.Empty).WithMessage("UserId must be a valid GUID.");

        RuleFor(x => x.Date)
            .NotEmpty().WithMessage("Date is required.")
            .LessThanOrEqualTo(DateTime.Now).WithMessage("Date cannot be in the future.");

        RuleFor(x => x.Products)
             .NotEmpty().WithMessage("Products list cannot be empty.")
             .Must(p => p != null && p.Count > 0).WithMessage("Products list must contain at least one item.")
             .ForEach(product =>
             {
                 product.NotNull().WithMessage("Product cannot be null.");
                 product.SetValidator(new CreateCartItemRequestValidator());
             });
    }
}


public class CreateCartItemRequestValidator : AbstractValidator<CreateCartItemRequest>
{

    public CreateCartItemRequestValidator()
    {
        RuleFor(item => item.ProductId)
            .NotEmpty().WithMessage("Product ID is required.");

        RuleFor(item => item.Quantity)
            .GreaterThan(0).WithMessage("Quantity must be greater than zero.");
    }
}
