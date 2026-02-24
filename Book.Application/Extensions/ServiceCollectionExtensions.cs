using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Book.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddMediatorAndAutoMapper(this IServiceCollection services, Assembly[] assemblies)
    {
        Assembly[] assemblies2 = assemblies;
        List<Assembly> list = new List<Assembly> { Assembly.GetExecutingAssembly() };
        if (assemblies2 != null && assemblies2.Any())
        {
            list.AddRange(assemblies2);
        }

        var assembliesArray = list.ToArray();

        services.AddMediatR(delegate (MediatRServiceConfiguration cfg)
        {
            cfg.RegisterServicesFromAssemblies(assembliesArray);
        });

        services.AddAutoMapper(assembliesArray);
    }
}
