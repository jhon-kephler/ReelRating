using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SelectedMovie.Data;
using SelectedMovie.Data.Repositories;
using SelectedMovie.Domain.Repository;
using Microsoft.Extensions.Configuration;

namespace SelectedMovie.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddContext(configuration);
            services.AddRepository();
            services.AddHandler();

            return services;
        }

        public static IServiceCollection AddWorker(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddContext(configuration);
            services.AddRepository();
            services.AddHandler();

            return services;
        }

        public static IServiceCollection AddContext(this IServiceCollection services, IConfiguration configuration) =>
            services.AddDbContext<SelectedMovieContext>(options => { options.UseOracle(configuration["ConnectionString:DefaultConnection"]); });

        private static IServiceCollection AddHandler(this IServiceCollection services)
        {
            services.AddMediatR(configuration => configuration.RegisterServicesFromAssembly(Application.AssemblyReference.Assembly));
            services.AddAutoMapper(cfg => cfg.AddMaps(Core.AssemblyReference.Assembly));

            return services;
        }

        private static IServiceCollection AddRepository(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped(typeof(DbContext), typeof(SelectedMovieContext));
            return services;
        }

    }
}
