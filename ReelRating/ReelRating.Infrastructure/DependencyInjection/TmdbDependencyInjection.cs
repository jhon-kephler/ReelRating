using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using ReelRating.Domain.Services;
using ReelRating.Infrastructure.Options;
using ReelRating.Infrastructure.Services;

namespace ReelRating.Infrastructure.DependencyInjection
{
    public static class TmdbDependencyInjection
    {
        public static IServiceCollection AddTmdb(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMemoryCache();

            services.AddOptions<TmdbOptions>()
                .Bind(configuration.GetSection(TmdbOptions.SectionName))
                .Validate(options =>
                        !string.IsNullOrWhiteSpace(options.ApiKey) &&
                        !string.IsNullOrWhiteSpace(options.BaseUrl) &&
                        !string.IsNullOrWhiteSpace(options.ImageBaseUrl) &&
                        options.SyncIntervalMinutes > 0,
                    "A configuração do TMDb está inválida.")
                .ValidateOnStart();

            services.AddHttpClient("Tmdb", (provider, client) =>
            {
                var options = provider.GetRequiredService<IOptions<TmdbOptions>>().Value;
                client.BaseAddress = new Uri(options.BaseUrl.TrimEnd('/') + "/");
            });

            services.AddScoped<ITmdbService, TmdbService>();

            return services;
        }
    }
}
