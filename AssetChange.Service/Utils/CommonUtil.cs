namespace AssetChange.Service.Utils
{
    public static class CommonUtil
    {
        public static DateTime ConvertFromTimestamp(double timestamp)
        {
            DateTime dateTime = new System.DateTime(1965, 1, 1, 0, 0, 0, 0);
            dateTime = dateTime.AddSeconds(timestamp);
            return dateTime;
        }
    }
}
