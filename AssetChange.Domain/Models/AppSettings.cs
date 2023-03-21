using Microsoft.Extensions.Configuration;

namespace AssetChange.Domain.Models
{
    public static class AppSettings
    {
        public static class Database
        {
            public static string ConnectionStrings { get => GetValueFromKey("Database:ConnectionStrings"); }
        }

        public static class ExternalServices
        {
            public static class YahooFinance
            {
                public static string UrlBase { get => GetValueFromKey("ExternalServices:YahooFinance:UrlBase"); }
            }
        }

        private static string GetValueFromKey(string key) => GetAppSettings().GetSection(key).Value;

        private static IConfigurationRoot GetAppSettings()
        {
            try
            {
                IConfigurationBuilder builder = new ConfigurationBuilder();
                builder.AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json"));
                IConfigurationRoot root = builder.Build();
                return root;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
