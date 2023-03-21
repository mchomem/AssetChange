namespace AssetChange.Domain.Entities
{
    // Relashionshp between 'meta', 'timestamp' and 'indicators.open'
    public class AssetTradingDate
    {
        public int? Id { get; set; }
        public int? AssetId { get; set; }
        public Asset? Asset { get; set; }
        public DateTime? EventDate { get; set; }
        public decimal? OpeningValue { get; set; }
    }
}
