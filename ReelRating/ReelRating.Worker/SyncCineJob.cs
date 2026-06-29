using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ReelRating.Data;
using ReelRating.Domain.Entities;
using ReelRating.Domain.Services;
using ReelRating.Infrastructure.Options;
using System.Globalization;

namespace ReelRating.Worker
{
    public class SyncCineJob : BackgroundService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly ILogger<SyncCineJob> _logger;
        private readonly TmdbOptions _options;

        public SyncCineJob(
            IServiceScopeFactory serviceScopeFactory,
            IOptions<TmdbOptions> options,
            ILogger<SyncCineJob> logger)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _logger = logger;
            _options = options.Value;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    await SyncAsync(stoppingToken);
                }
                catch (OperationCanceledException) when (stoppingToken.IsCancellationRequested)
                {
                    break;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Erro durante sincronização com TMDb.");
                }

                await Task.Delay(
                    TimeSpan.FromMinutes(_options.SyncIntervalMinutes),
                    stoppingToken);
            }
        }

        private async Task SyncAsync(CancellationToken cancellationToken)
        {
            using var scope = _serviceScopeFactory.CreateScope();

            var tmdbService = scope.ServiceProvider.GetRequiredService<ITmdbService>();
            var context = scope.ServiceProvider.GetRequiredService<ReelRatingContext>();

            var categories = await context.Categories
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            var tmdbGenres = await tmdbService.GetMovieGenresAsync(cancellationToken);
            var genreMappings = BuildGenreMappings(categories, tmdbGenres);

            var inserted = 0;
            var updated = 0;
            var notesInserted = 0;
            var notesUpdated = 0;
            var categoryLinksInserted = 0;
            var errors = 0;

            var currentPage = 1;
            var totalPages = 1;

            while (currentPage <= totalPages && !cancellationToken.IsCancellationRequested)
            {
                var page = await tmdbService.DiscoverMoviesAsync(currentPage, cancellationToken);
                totalPages = page.TotalPages;

                foreach (var movie in page.Movies)
                {
                    try
                    {
                        if (string.IsNullOrWhiteSpace(movie.Title))
                        {
                            _logger.LogWarning(
                                "Filme TMDb {TmdbId} ignorado sem título.",
                                movie.TmdbId);

                            continue;
                        }

                        var cine = await context.Cine
                            .FirstOrDefaultAsync(x => x.TmdbId == movie.TmdbId, cancellationToken);

                        var isNew = cine is null;

                        if (isNew)
                        {
                            cine = new Cine();
                            await context.Cine.AddAsync(cine, cancellationToken);
                            inserted++;
                        }
                        else
                        {
                            updated++;
                        }

                        if (cine is null)
                            throw new InvalidOperationException($"Erro ao inicializar filme {movie.TmdbId}");

                        // =====================================================
                        // 🎬 DADOS DO FILME
                        // =====================================================
                        cine.Name = movie.Title;
                        cine.TmdbId = movie.TmdbId;
                        cine.TypeId = _options.MovieTypeId;
                        cine.URLPoster = movie.PosterUrl;

                        // 🔥 CORREÇÃO DO RELEASE DATE
                        if (movie.ReleaseDate.HasValue && movie.ReleaseDate.Value.Year > 1900)
                        {
                            cine.Year = movie.ReleaseDate.Value.Year;
                            cine.Month = movie.ReleaseDate.Value.Month;
                        }
                        else
                        {
                            cine.Year = null;
                            cine.Month = null;

                            _logger.LogWarning(
                                "TMDb filme {TmdbId} com data inválida: {ReleaseDate}",
                                movie.TmdbId,
                                movie.ReleaseDate);
                        }

                        // =====================================================
                        // 🔥 SALVA PRIMEIRO PARA GERAR Cine.Id (OBRIGATÓRIO)
                        // =====================================================
                        await context.SaveChangesAsync(cancellationToken);

                        // =====================================================
                        // 📝 NOTES
                        // =====================================================
                        var note = await context.Notes
                            .FirstOrDefaultAsync(x => x.CineId == cine.Id, cancellationToken);

                        if (note is null)
                        {
                            note = new Notes
                            {
                                CineId = cine.Id
                            };

                            await context.Notes.AddAsync(note, cancellationToken);
                            notesInserted++;
                        }
                        else
                        {
                            notesUpdated++;
                        }

                        note.TMDBNote = movie.VoteAverage.ToString("0.0", CultureInfo.InvariantCulture);

                        // =====================================================
                        // 🎭 CATEGORIES
                        // =====================================================
                        foreach (var genreId in movie.GenreIds.Distinct())
                        {
                            if (!genreMappings.TryGetValue(genreId, out var categoryId))
                            {
                                _logger.LogWarning(
                                    "Gênero TMDb {GenreId} sem correspondência.",
                                    genreId);

                                continue;
                            }

                            var exists = await context.Cine_Categories.AnyAsync(
                                x => x.CineId == cine.Id && x.CategoriesId == categoryId,
                                cancellationToken);

                            if (exists)
                                continue;

                            await context.Cine_Categories.AddAsync(new CineCategories
                            {
                                CineId = cine.Id,
                                CategoriesId = categoryId
                            }, cancellationToken);

                            categoryLinksInserted++;
                        }

                        // =====================================================
                        // 💾 SALVA RELAÇÕES (NOTES + CATEGORIAS)
                        // =====================================================
                        await context.SaveChangesAsync(cancellationToken);
                    }
                    catch (Exception ex)
                    {
                        errors++;

                        _logger.LogError(
                            ex,
                            "Erro ao sincronizar filme TMDb {TmdbId}",
                            movie.TmdbId);
                    }
                }

                currentPage++;
            }

            _logger.LogInformation(
                "Sync finalizado. Inseridos: {Inserted}, Atualizados: {Updated}, Notes Inseridas: {NotesInserted}, Notes Atualizadas: {NotesUpdated}, Categorias: {CategoryLinksInserted}, Erros: {Errors}",
                inserted,
                updated,
                notesInserted,
                notesUpdated,
                categoryLinksInserted,
                errors);
        }

        private Dictionary<int, int> BuildGenreMappings(
            IReadOnlyCollection<Categories> categories,
            IReadOnlyCollection<ReelRating.Domain.Services.Models.TmdbGenre> tmdbGenres)
        {
            var categoriesByName = categories
                .GroupBy(c => Normalize(c.Name))
                .ToDictionary(g => g.Key, g => g.First());

            var mappings = new Dictionary<int, int>();

            foreach (var genre in tmdbGenres)
            {
                var name = Normalize(genre.Name);

                if (categoriesByName.TryGetValue(name, out var category))
                {
                    mappings[genre.Id] = category.Id;
                }
                else
                {
                    _logger.LogWarning(
                        "Gênero TMDb {GenreName} ({GenreId}) não encontrado.",
                        genre.Name,
                        genre.Id);
                }
            }

            return mappings;
        }

        private static string Normalize(string? value)
            => (value ?? string.Empty).Trim().ToUpperInvariant();
    }
}