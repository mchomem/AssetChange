using System.Text.Json.Serialization;

namespace AssetChange.Domain.Dtos.External
{
    [Serializable]
    public class IndicatorDto
    {
        [JsonPropertyName("quote")]
        public List<IndicatorDtoQuote>? IndicatorDtoQuote { get; set; }
    }

    [Serializable]
    public class IndicatorDtoQuote
    {
        [JsonPropertyName("volume")]
        public List<int?>? Volune { get; set; }

        [JsonPropertyName("low")]
        public List<decimal?>? Low { get; set; }

        [JsonPropertyName("open")]
        public List<decimal?>? Open { get; set; }

        [JsonPropertyName("close")]
        public List<decimal?>? Close { get; set; }

        [JsonPropertyName("high")]
        public List<decimal?>? High { get; set; }
    }
}
