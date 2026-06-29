using System.Text.Json.Serialization;

public class TmdbDiscoverMovie
{
    [JsonPropertyName("id")]
    public int TmdbId { get; set; }

    [JsonPropertyName("title")]
    public string Title { get; set; } = string.Empty;

    [JsonPropertyName("release_date")]
    public DateTime? ReleaseDate { get; set; }

    [JsonPropertyName("poster_path")]
    public string? PosterUrl { get; set; }

    [JsonPropertyName("vote_average")]
    public decimal VoteAverage { get; set; }

    [JsonPropertyName("genre_ids")]
    public IReadOnlyCollection<int> GenreIds { get; set; } = [];
}