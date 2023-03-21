using System.Text.Json.Serialization;

namespace AssetChange.Domain.Dtos.External
{
    [Serializable]
    public class AssetDto
    {
        [JsonPropertyName("currency")]
        public string? Currency { get; set; }

        [JsonPropertyName("symbol")]
        public string? Symbol { get; set; }

        [JsonPropertyName("exchangeName")]
        public string? ExchangeName { get; set; }

        [JsonPropertyName("instrumentType")]
        public string? InstrumentType { get; set; }

        [JsonPropertyName("firstTradeDate")]
        public double FirstTradeDate { get; set; }

        [JsonPropertyName("regularMarketTime")]
        public double RegularMarketTime { get; set; }

        [JsonPropertyName("gmtoffset")]
        public int Gmtoffset { get; set; }

        [JsonPropertyName("timezone")]
        public string? Timezone { get; set; }

        [JsonPropertyName("exchangeTimezoneName")]
        public string? ExchangeTimezoneName { get; set; }

        [JsonPropertyName("regularMarketPrice")]
        public decimal RegularMarketPrice { get; set; }

        [JsonPropertyName("chartPreviousClose")]
        public decimal ChartPreviousClose { get; set; }

        [JsonPropertyName("previousClose")]
        public decimal PreviousClose { get; set; }

        [JsonPropertyName("scale")]
        public int Scale { get; set; }

        [JsonPropertyName("priceHint")]
        public int PriceHint { get; set; }

        [JsonPropertyName("currentTradingPeriod")]
        public CurrentTradingPeriodDto? CurrentTradingPeriod { get; set; }
        
        [JsonPropertyName("tradingPeriods")]
        public List<List<TradingPeriodDto>>? TradingPeriods { get; set; }

        [JsonPropertyName("dataGranularity")]
        public string? DataGranularity { get; set; }

        [JsonPropertyName("range")]
        public string? Range { get; set; }

        [JsonPropertyName("validRanges")]
        public List<string>? ValidRanges { get; set; }
    }

    [Serializable]
    public class CurrentTradingPeriodPreDto
    {
        [JsonPropertyName("timezone")]
        public string? Timezone { get; set; }

        [JsonPropertyName("end")]
        public double End { get; set; }

        [JsonPropertyName("start")]
        public double Start { get; set; }

        [JsonPropertyName("gmtoffset")]
        public int Gmtoffset { get; set; }
    }

    [Serializable]
    public class CurrentTradingPeriodRegularDto
    {
        [JsonPropertyName("timezone")]
        public string? Timezone { get; set; }

        [JsonPropertyName("end")]
        public double End { get; set; }

        [JsonPropertyName("start")]
        public double Start { get; set; }

        [JsonPropertyName("gmtoffset")]
        public int Gmtoffset { get; set; }
    }

    [Serializable]
    public class CurrentTradingPeriodPostDto
    {
        [JsonPropertyName("timezone")]
        public string? Timezone { get; set; }

        [JsonPropertyName("end")]
        public double End { get; set; }

        [JsonPropertyName("start")]
        public double Start { get; set; }

        [JsonPropertyName("gmtoffset")]
        public int Gmtoffset { get; set; }
    }

    [Serializable]
    public class CurrentTradingPeriodDto
    {
        [JsonPropertyName("pre")]
        public CurrentTradingPeriodPreDto? CurrentTradingPeriodPre { get; set; }
        [JsonPropertyName("regular")]
        public CurrentTradingPeriodRegularDto? CurrentTradingPeriodRegular { get; set; }
        [JsonPropertyName("post")]
        public CurrentTradingPeriodPostDto? CurrentTradingPeriodPost { get; set; }
    }

    [Serializable]
    public class TradingPeriodDto
    {
        [JsonPropertyName("timezone")]
        public string? Timezone { get; set; }

        [JsonPropertyName("end")]
        public double End { get; set; }

        [JsonPropertyName("start")]
        public double Start { get; set; }

        [JsonPropertyName("gmtoffset")]
        public int Gmtoffset { get; set; }
    }
}
