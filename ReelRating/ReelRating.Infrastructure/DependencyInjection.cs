using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ReelRating.Application.Services.AuthServices;
using ReelRating.Application.Services.AuthServices.Interfaces;
using ReelRating.Application.Services.HomeServices;
using ReelRating.Application.Services.HomeServices.Interfaces;
using ReelRating.Data;
using ReelRating.Data.Command.AuthCommand;
using ReelRating.Data.Query.AuthQuery;
using ReelRating.Data.Query.FiltersQuery;
using ReelRating.Data.Repositories;
using ReelRating.Domain.Repository;

namespace ReelRating.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddContext(configuration);
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
            services.AddHandler();

            return services;
        }

        public static IServiceCollection AddContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ReelRatingContext>(options =>
                options.UseOracle(configuration.GetConnectionString("DefaultConnection")));

            return services;
        }

        private static IServiceCollection AddHandler(this IServiceCollection services)
        {
            services.AddMediatR(configuration => configuration.RegisterServicesFromAssembly(Application.AssemblyReference.Assembly));
            services.AddAutoMapper(cfg => cfg.AddMaps(Core.AssemblyReference.Assembly));

            return services;
        }

        private static IServiceCollection AddRepository(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped(typeof(DbContext), typeof(ReelRatingContext));
            return services;
        }
        
        private static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<ISearchFiltersServices,SearchFiltersServices>();
            services.AddScoped<IManageAuthService, ManageAuthService>();
            services.AddScoped<ISearchAuthService, SearchAuthService>();

            return services;
        }

        private static IServiceCollection AddQuery(this IServiceCollection services)
        {
            services.AddScoped<IGetListCategories, GetListCategoriesQuery>();
            services.AddScoped<IGetCustomerQuery, GetCustomerQuery>();

            return services;
        }   

        private static IServiceCollection AddCommand(this IServiceCollection services)
        {
            services.AddScoped<ICreateCommand, CreateCommand>();
            return services;
        }

    }
}
