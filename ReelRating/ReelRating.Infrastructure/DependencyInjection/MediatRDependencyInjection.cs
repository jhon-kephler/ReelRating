using Microsoft.Extensions.DependencyInjection;

namespace ReelRating.Infrastructure.DependencyInjection
{
    public static class MediatRDependencyInjection
    {
        public static IServiceCollection AddHandler(this IServiceCollection services)
        {
            services.AddMediatR(configuration => configuration.RegisterServicesFromAssembly(Application.AssemblyReference.Assembly));
            services.AddAutoMapper(cfg => cfg.AddMaps(Core.AssemblyReference.Assembly));

            return services;
        }
    }
}
