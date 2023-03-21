namespace AssetChange.Domain.Entities
{
    public class CurrentTradingPeriod
    {
        public int? Id { get; set; }
        public int? AssetId { get; set; }
        public Asset? Asset { get; set; }
        public string? Timezone { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public int Gmtoffset { get; set; }
        public string? Type { get; set; }
    }

    public class CurrentTradingPeriodType
    { 
        public const string PRE = "PRE";
        public const string REGULAR = "REGULAR";
        public const string POST = "POST";       
    }
}
