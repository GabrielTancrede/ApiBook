using Book.Application.Interfaces.Repositories;
using Book.Infra.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Book.Api.Configurations
{
    public static class ServicesConfig
    {
        public static void ConfigureBaseServices(this IServiceCollection services)
        {
            services.AddHttpClient();
            services.AddScoped<IGenreRepository, GenreRepository>();
            services.AddScoped<IAuthorRepository, AuthorRepository>();
            services.AddScoped<IBookRepository, BookRepository>();
        }

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
}
