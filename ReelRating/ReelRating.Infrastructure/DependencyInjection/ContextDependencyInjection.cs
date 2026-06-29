using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ReelRating.Data;

namespace ReelRating.Infrastructure.DependencyInjection
{
    public static class ContextDependencyInjection
    {
        public static IServiceCollection AddContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ReelRatingContext>(options =>
                options.UseOracle(configuration.GetConnectionString("DefaultConnection")));

            return services;
        }
    }
}
