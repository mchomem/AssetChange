namespace AssetChange.Infra.Data.Utils
{
    public static class CommonUtil
    {
        public static DateTime ConvertFromTimestamp(double timestamp)
        {
            DateTime dateTime = new System.DateTime(1965, 1, 1, 0, 0, 0, 0);
            dateTime = dateTime.AddSeconds(timestamp);
            return dateTime;
        }

        public static string CalculatePriceChange(decimal? newValue, decimal? oldValue)
            => $" {((newValue - oldValue) / oldValue) * 100}%";
    }
}
