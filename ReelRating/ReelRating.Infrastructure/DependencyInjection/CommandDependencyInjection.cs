using Microsoft.Extensions.DependencyInjection;
using ReelRating.Data.Command.AuthCommand;
using ReelRating.Data.Query.CineQuery;

namespace ReelRating.Infrastructure.DependencyInjection
{
    public static class CommandDependencyInjection
    {
        public static IServiceCollection AddCommand(this IServiceCollection services)
        {
            services.Scan(scan => scan
                    .FromAssemblyOf<ICreateCommand>()
                    .AddClasses(classes => classes.Where(t =>
                        t.Name.EndsWith("Command")))
                    .AsImplementedInterfaces()
                    .WithScopedLifetime());

            return services;
        }
    }
}
