namespace ReelRating.Domain.Services.Models
{
    public class TmdbDiscoverPage
    {
        public int Page { get; set; }
        public int TotalPages { get; set; }
        public IReadOnlyCollection<TmdbDiscoverMovie> Movies { get; set; } = [];
    }
}