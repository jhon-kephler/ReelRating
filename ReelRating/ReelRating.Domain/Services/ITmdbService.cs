using ReelRating.Domain.Services.Models;

namespace ReelRating.Domain.Services
{
    public interface ITmdbService
    {
        Task<TmdbDiscoverPage> DiscoverMoviesAsync(int page, CancellationToken cancellationToken);
        Task<IReadOnlyCollection<TmdbGenre>> GetMovieGenresAsync(CancellationToken cancellationToken);
    }
}