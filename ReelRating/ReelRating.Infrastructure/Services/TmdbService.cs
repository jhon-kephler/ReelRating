using System.Globalization;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using ReelRating.Domain.Services;
using ReelRating.Domain.Services.Models;
using ReelRating.Infrastructure.Options;
using ReelRating.Infrastructure.Services.Dtos;

namespace ReelRating.Infrastructure.Services
{
    public class TmdbService : ITmdbService
    {
        private static readonly JsonSerializerOptions JsonOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };

        private readonly IHttpClientFactory _httpClientFactory;
        private readonly TmdbOptions _options;
        private readonly ILogger<TmdbService> _logger;
        private readonly IMemoryCache _memoryCache;
        private const string MovieGenresCacheKey = "tmdb_movie_genres_pt_br";

        public TmdbService(
            IHttpClientFactory httpClientFactory,
            IOptions<TmdbOptions> options,
            ILogger<TmdbService> logger,
            IMemoryCache memoryCache)
        {
            _httpClientFactory = httpClientFactory;
            _options = options.Value;
            _logger = logger;
            _memoryCache = memoryCache;
        }

        public async Task<TmdbDiscoverPage> DiscoverMoviesAsync(int page, CancellationToken cancellationToken)
        {
            var client = _httpClientFactory.CreateClient("Tmdb");
            var requestUri = $"discover/movie?api_key={Uri.EscapeDataString(_options.ApiKey)}&language=pt-BR&page={page}";

            using var response = await client.GetAsync(requestUri, cancellationToken);
            if (!response.IsSuccessStatusCode)
            {
                var body = await response.Content.ReadAsStringAsync(cancellationToken);
                _logger.LogError("Falha ao consultar TMDb. Status: {StatusCode}. Body: {Body}", response.StatusCode, body);
                response.EnsureSuccessStatusCode();
            }

            await using var stream = await response.Content.ReadAsStreamAsync(cancellationToken);
            var payload = await JsonSerializer.DeserializeAsync<TmdbDiscoverResponseDto>(stream, JsonOptions, cancellationToken);

            if (payload is null)
            {
                throw new InvalidOperationException("Resposta do TMDb vazia ou inválida.");
            }

            return new TmdbDiscoverPage
            {
                Page = payload.Page,
                TotalPages = payload.TotalPages,
                Movies = payload.Results?.Select(MapMovie).ToList() ?? []
            };
        }

        public async Task<IReadOnlyCollection<TmdbGenre>> GetMovieGenresAsync(CancellationToken cancellationToken)
        {
            if (_memoryCache.TryGetValue<IReadOnlyCollection<TmdbGenre>>(MovieGenresCacheKey, out var cachedGenres) &&
                cachedGenres is not null)
            {
                return cachedGenres;
            }

            var client = _httpClientFactory.CreateClient("Tmdb");
            var requestUri = $"genre/movie/list?api_key={Uri.EscapeDataString(_options.ApiKey)}&language=pt-BR";

            using var response = await client.GetAsync(requestUri, cancellationToken);
            if (!response.IsSuccessStatusCode)
            {
                var body = await response.Content.ReadAsStringAsync(cancellationToken);
                _logger.LogError("Falha ao consultar gêneros do TMDb. Status: {StatusCode}. Body: {Body}", response.StatusCode, body);
                response.EnsureSuccessStatusCode();
            }

            await using var stream = await response.Content.ReadAsStreamAsync(cancellationToken);
            var payload = await JsonSerializer.DeserializeAsync<TmdbGenreListResponseDto>(stream, JsonOptions, cancellationToken);

            if (payload?.Genres is null)
            {
                throw new InvalidOperationException("Resposta de gêneros do TMDb vazia ou inválida.");
            }

            var genres = payload.Genres
                .Select(genre => new TmdbGenre
                {
                    Id = genre.Id,
                    Name = genre.Name?.Trim() ?? string.Empty
                })
                .Where(genre => !string.IsNullOrWhiteSpace(genre.Name))
                .ToList()
                .AsReadOnly();

            _memoryCache.Set(MovieGenresCacheKey, genres, TimeSpan.FromHours(12));

            return genres;
        }

        private TmdbDiscoverMovie MapMovie(TmdbDiscoverMovieDto dto)
        {
            DateTime? releaseDate = null;
            if (!string.IsNullOrWhiteSpace(dto.ReleaseDate) &&
                DateTime.TryParse(dto.ReleaseDate, CultureInfo.InvariantCulture, DateTimeStyles.None, out var parsedDate))
            {
                releaseDate = parsedDate;
            }

            return new TmdbDiscoverMovie
            {
                TmdbId = dto.Id,
                Title = dto.Title?.Trim() ?? string.Empty,
                ReleaseDate = releaseDate,
                PosterUrl = BuildPosterUrl(dto.PosterPath),
                VoteAverage = dto.VoteAverage,
                GenreIds = dto.GenreIds ?? []
            };
        }

        private string? BuildPosterUrl(string? posterPath)
        {
            if (string.IsNullOrWhiteSpace(posterPath))
            {
                return null;
            }

            return $"{_options.ImageBaseUrl.TrimEnd('/')}/{posterPath.TrimStart('/')}";
        }
    }
}