namespace ReelRating.Infrastructure.Options
{
    public class TmdbOptions
    {
        public const string SectionName = "Tmdb";

        public string ApiKey { get; set; } = string.Empty;
        public string BaseUrl { get; set; } = string.Empty;
        public string ImageBaseUrl { get; set; } = string.Empty;
        public int SyncIntervalMinutes { get; set; } = 60;
        public int MovieTypeId { get; set; } = 1;
    }
}