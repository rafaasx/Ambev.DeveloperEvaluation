using Ambev.DeveloperEvaluation.IoC.ModuleInitializers;
using Microsoft.AspNetCore.Builder;
using System.Reflection;

namespace Ambev.DeveloperEvaluation.IoC;

public static class DependencyResolver
{
    public static void RegisterDependencies(this WebApplicationBuilder builder, List<Type> filters)
    {
        new ApplicationModuleInitializer().Initialize(builder);
        new InfrastructureModuleInitializer().Initialize(builder);
        new WebApiModuleInitializer().Initialize(builder, filters);
        var assembly = Assembly.GetExecutingAssembly();
    }
}