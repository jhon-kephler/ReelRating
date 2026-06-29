using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ReelRating.Data;
using ReelRating.Data.Repositories;
using ReelRating.Domain.Repositories;
using ReelRating.Domain.Repository;

namespace ReelRating.Infrastructure.DependencyInjection
{
    public static class RepositoryDependencyInjection
    {
        public static IServiceCollection AddRepository(this IServiceCollection services)
        {
            services.AddScoped(typeof(DbContext), typeof(ReelRatingContext));

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped(typeof(ICineRepository), typeof(CineRepository));

            return services;
        }
    }
}
