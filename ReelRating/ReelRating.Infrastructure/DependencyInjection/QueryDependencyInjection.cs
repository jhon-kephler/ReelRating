using Microsoft.Extensions.DependencyInjection;
using ReelRating.Data.Query.CineQuery;

namespace ReelRating.Infrastructure.DependencyInjection
{
    public static class QueryDependencyInjection
    {
        public static IServiceCollection AddQuery(this IServiceCollection services)
        {
            services.Scan(scan => scan
                    .FromAssemblyOf<IListYearQuery>()
                    .AddClasses(classes => classes.Where(t =>
                        t.Name.EndsWith("Query")))
                    .AsImplementedInterfaces()
                    .WithScopedLifetime());

            return services;
        }
    }
}
