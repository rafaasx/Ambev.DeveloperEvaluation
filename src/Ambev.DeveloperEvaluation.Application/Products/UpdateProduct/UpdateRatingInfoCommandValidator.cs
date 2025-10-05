using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Products.UpdateProduct;

public class UpdateRatingInfoCommandValidator : AbstractValidator<UpdateRatingInfoCommand>
{
    public UpdateRatingInfoCommandValidator()
    {
        RuleFor(x => x.Rate)
            .GreaterThanOrEqualTo(0).WithMessage("Rate cannot be negative.");

        RuleFor(x => x.Count)
            .GreaterThanOrEqualTo(0).WithMessage("Count cannot be negative.");
    }
}
