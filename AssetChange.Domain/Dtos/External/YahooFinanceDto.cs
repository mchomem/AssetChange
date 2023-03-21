using System.Text.Json.Serialization;

namespace AssetChange.Domain.Dtos.External
{
    [Serializable]
    public class YahooFinanceDto
    {
        [JsonPropertyName("meta")]
        public AssetDto? AssetDto { get; set; }

        [JsonPropertyName("timestamp")]
        public List<double>? Timestamp { get; set; }

        [JsonPropertyName("indicators")]
        public IndicatorDto? IndicatorDto { get; set; }
    }

    [Serializable]
    public class YahooFinanceResultDto
    {
        [JsonPropertyName("result")]
        public List<YahooFinanceDto>? YahooFinanceDto { get; set; }

        [JsonPropertyName("error")]
        public string? Error { get; set; }
    }

    [Serializable]

    public class YahooChartDto
    {
        [JsonPropertyName("chart")]
        public YahooFinanceResultDto? YahooFinanceResultDto { get; set; }
    }
}
