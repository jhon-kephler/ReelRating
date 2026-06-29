using Microsoft.Extensions.DependencyInjection;
using ReelRating.Application.Services.AuthServices.Interfaces;

namespace ReelRating.Infrastructure.DependencyInjection
{
    public static class ServiceDependencyInjection
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.Scan(scan => scan
                    .FromAssemblyOf<ITokenService>()
                    .AddClasses(classes => classes.Where(t => t.Name.EndsWith("Service")))
                    .AsImplementedInterfaces()
                    .WithScopedLifetime());

            return services;
        }
    }
}
