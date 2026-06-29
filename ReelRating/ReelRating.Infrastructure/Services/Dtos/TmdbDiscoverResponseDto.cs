using System.Text.Json.Serialization;

namespace ReelRating.Infrastructure.Services.Dtos
{
    internal sealed class TmdbDiscoverResponseDto
    {
        [JsonPropertyName("page")]
        public int Page { get; set; }

        [JsonPropertyName("total_pages")]
        public int TotalPages { get; set; }

        [JsonPropertyName("results")]
        public List<TmdbDiscoverMovieDto>? Results { get; set; }
    }
}