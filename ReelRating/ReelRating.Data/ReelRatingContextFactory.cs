using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace ReelRating.Data
{
    /// <summary>
    /// Permite que o dotnet-ef instancie o DbContext em design-time
    /// (migrations add, database update) sem precisar da aplicação rodando.
    /// </summary>
    public class ReelRatingContextFactory : IDesignTimeDbContextFactory<ReelRatingContext>
    {
        public ReelRatingContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "..", "ReelRating.API"))
                .AddJsonFile("appsettings.json", optional: false)
                .AddJsonFile("appsettings.Development.json", optional: true)
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<ReelRatingContext>();
            optionsBuilder.UseOracle(configuration.GetConnectionString("DefaultConnection"));

            return new ReelRatingContext(optionsBuilder.Options);
        }
    }
}
