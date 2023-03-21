namespace AssetChange.Infra.Data.Utils
{
    public static class BusinessUtil
    {
        public static string CalculateAndFormatPriceChange(decimal? newValue, decimal? oldValue)
        {
            var result = ((newValue - oldValue) / oldValue * 100);
            return result != null ? $"{ result ?.ToString("0.00") }%" : "-";
        }
    }
}
