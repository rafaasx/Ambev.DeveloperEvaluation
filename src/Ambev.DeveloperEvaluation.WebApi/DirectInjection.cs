using Ambev.DeveloperEvaluation.WebApi.Features.Users.CreateUser;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi;

public static class DirectInjection
{
    public static void RegisterValidators(this WebApplicationBuilder builder)
    {
        var assembly = typeof(CreateUserRequestValidator).Assembly;
        var validators = assembly.GetTypes()
        .Where(t => !t.IsAbstract && !t.IsInterface)
        .SelectMany(t => t.GetInterfaces()
            .Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IValidator<>))
            .Select(i => new { ValidatorType = t, InterfaceType = i }));

        foreach (var v in validators)
        {
            builder.Services.AddScoped(v.InterfaceType, v.ValidatorType);
        }
    }
}
