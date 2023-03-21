namespace AssetChange.Domain.Entities
{
    public class Asset
    {
        public int? Id { get; set; }
        public DateTime? ImportedIn { get; set; }
        public string? Currency { get; set; }
        public string? Symbol { get; set; }
        public string? ExchangeName { get; set; }
        public string? InstrumentType { get; set; }
        public DateTime? FirstTradeDate { get; set; }
        public DateTime? RegularMarketTime { get; set; }
        public int? Gmtoffset { get; set; }
        public string? Timezone { get; set; }
        public string? ExchangeTimezoneName { get; set; }
        public decimal? RegularMarketPrice { get; set; }
        public decimal? ChartPreviousClose { get; set; }
        public decimal? PreviousClose { get; set; }
        public int? Scale { get; set; }
        public int? PriceHint { get; set; }
        public string? DataGranularity { get; set; }
        public string? Range { get; set; }
        public string? ValidRanges { get; set; }
        public ICollection<CurrentTradingPeriod>? CurrentTradingPeriods { get; set; }
        public ICollection<AssetTradingDate>? AssetTradingDates { get; set; }
    }
}
