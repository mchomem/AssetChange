using AssetChange.Domain.Dtos.External;
using AssetChange.Domain.Models;
using AssetChange.Service.Services.Interfaces;
using System.Text.Json;

namespace AssetChange.Service.Services.External
{
    public class YahooFinanceService : ExternalBaseService
    {
        private readonly IAssetService _assetService;

        public YahooFinanceService(IAssetService assetService)
            => _assetService = assetService;

        public async Task<YahooChartDto?> GetFullData(string assetName)
        {
            string result = await Get($"{AppSettings.ExternalServices.YahooFinance.UrlBase}/{assetName}");
            YahooChartDto? yahooJson = JsonSerializer.Deserialize<YahooChartDto>(result);
            return yahooJson != null ? yahooJson : null;
        }

        public async Task ImportData(string assetName)
        {
            string result = await Get($"{AppSettings.ExternalServices.YahooFinance.UrlBase}/{assetName}");
            YahooChartDto? yahooChart = JsonSerializer.Deserialize<YahooChartDto>(result);

            if (yahooChart == null)
                throw new Exception("No information found on Yahoo Finance API.");

            await TransformAndSave(yahooChart);
        }

        private async Task TransformAndSave(YahooChartDto? yahooChart)
            => await _assetService.AddAsync(yahooChart);
    }
}
