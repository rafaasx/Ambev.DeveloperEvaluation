using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Ambev.DeveloperEvaluation.WebApi.Filters;

public class ValidationFilter : IAsyncActionFilter
{
    private readonly IServiceProvider _serviceProvider;

    public ValidationFilter(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        foreach (var argument in context.ActionArguments.Values)
        {
            if (argument == null) continue;

            var validatorType = typeof(IValidator<>).MakeGenericType(argument.GetType());
            var validator = _serviceProvider.GetService(validatorType);

            if (validator is IValidator baseValidator)
            {
                var result = await baseValidator.ValidateAsync(new ValidationContext<object>(argument));

                if (!result.IsValid)
                {
                    context.Result = new BadRequestObjectResult(result.Errors);
                    return;
                }
            }
        }

        await next();
    }
}
