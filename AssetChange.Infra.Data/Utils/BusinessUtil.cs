namespace AssetChange.Infra.Data.Utils
{
    public static class BusinessUtil
    {
        public static string CalculateAndFormatPriceChange(decimal? newValue, decimal? oldValue)
            => $" {((newValue - oldValue) / oldValue * 100)?.ToString("0.00")}%";
    }
}
