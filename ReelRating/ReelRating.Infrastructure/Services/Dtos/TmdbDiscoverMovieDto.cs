using System.Text.Json.Serialization;

namespace ReelRating.Infrastructure.Services.Dtos
{
    internal sealed class TmdbDiscoverMovieDto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("title")]
        public string? Title { get; set; }

        [JsonPropertyName("release_date")]
        public string? ReleaseDate { get; set; }

        [JsonPropertyName("poster_path")]
        public string? PosterPath { get; set; }

        [JsonPropertyName("vote_average")]
        public decimal VoteAverage { get; set; }

        [JsonPropertyName("genre_ids")]
        public List<int>? GenreIds { get; set; }
    }
}