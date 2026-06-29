using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ReelRating.Infrastructure.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddContext(configuration);
            services.AddAuth(configuration);
            services.AddRepository();
            services.AddHandler();
            services.AddServices();
            services.AddQuery();
            services.AddCommand();

            return services;
        }

        public static IServiceCollection AddWorker(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddContext(configuration);
            services.AddRepository();
            services.AddTmdb(configuration);

            return services;
        }
    }
}