using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Ambev.DeveloperEvaluation.IoC.ModuleInitializers;

public class WebApiModuleInitializer : IModuleInitializer
{
    public void Initialize(WebApplicationBuilder builder, List<Type> filters = default)
    {

        builder.Services.AddControllers(options =>
        {
            foreach (Type filter in filters)
                options.Filters.Add(filter);
        });
        builder.Services.AddHealthChecks();
    }
}
