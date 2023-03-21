namespace AssetChange.Domain.Dtos
{
    [Serializable]
    public class AssetChangeDto
    {
        public int? Id { get; set; }
        public DateTime? EventData { get; set; }
        public decimal? OpeningValue { get; set; }
        public string? PercentageD1 { get; set; }
        public string? PercentageFirstDay { get; set; }
    }
}
