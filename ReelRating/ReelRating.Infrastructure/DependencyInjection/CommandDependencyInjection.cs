using Microsoft.Extensions.DependencyInjection;
using ReelRating.Data.Command.AuthCommand;

namespace ReelRating.Infrastructure.DependencyInjection
{
    public static class CommandDependencyInjection
    {
        public static IServiceCollection AddCommand(this IServiceCollection services)
        {
            services.AddScoped<ICreateCommand, CreateCommand>();
            return services;
        }
    }
}
