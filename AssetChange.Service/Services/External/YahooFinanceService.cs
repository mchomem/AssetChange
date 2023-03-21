using AssetChange.Domain.Dtos.External;
using AssetChange.Domain.Entities;
using AssetChange.Domain.Models;
using AssetChange.Service.Services.Interfaces;
using AssetChange.Service.Utils;
using System.Text.Json;

namespace AssetChange.Service.Services.External
{
    public class YahooFinanceService : ExternalBaseService
    {
        private readonly IAssetService _assetService;
        private readonly IAssetTradingDateService _assetTradingDataService;

        public YahooFinanceService(IAssetService assetService, IAssetTradingDateService assetTradingDataService)
        {
            _assetService = assetService;
            _assetTradingDataService = assetTradingDataService;
        }

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
        {
            foreach (YahooFinanceDto item in yahooChart.YahooFinanceResultDto.YahooFinanceDto)
            {
                Asset asset = new Asset();
                asset.ImportedIn = DateTime.Now;
                asset.Currency = item.AssetDto.Currency;
                asset.Symbol = item.AssetDto.Symbol;
                asset.ExchangeName = item.AssetDto.ExchangeName;
                asset.InstrumentType = item.AssetDto.InstrumentType;
                asset.FirstTradeDate = CommonUtil.ConvertFromTimestamp(item.AssetDto.FirstTradeDate);
                asset.RegularMarketTime = CommonUtil.ConvertFromTimestamp(item.AssetDto.RegularMarketTime);
                asset.Gmtoffset = item.AssetDto.Gmtoffset;
                asset.Timezone = item.AssetDto.Timezone;
                asset.ExchangeTimezoneName = item.AssetDto.ExchangeTimezoneName;
                asset.RegularMarketPrice = item.AssetDto.RegularMarketPrice;
                asset.ChartPreviousClose = item.AssetDto.ChartPreviousClose;
                asset.PreviousClose = item.AssetDto.PreviousClose;
                asset.Scale = item.AssetDto.Scale;
                asset.PriceHint = item.AssetDto.PriceHint;
                asset.DataGranularity = item.AssetDto.DataGranularity;
                asset.Range = item.AssetDto.Range;
                asset.ValidRanges = string.Join(",", item.AssetDto.ValidRanges);

                await _assetService.AddAsync(asset);

                for (int i = 0; i < item.Timestamp.Count; i++)
                {
                    DateTime? eventDate = CommonUtil.ConvertFromTimestamp(item.Timestamp[i]);
                    decimal? openingValue = item.IndicatorDto.IndicatorDtoQuote[0].Open[i];

                    AssetTradingDate assetTrading = new AssetTradingDate();
                    assetTrading.AssetId = asset.Id;
                    assetTrading.Asset = asset;
                    assetTrading.EventDate = eventDate;
                    assetTrading.OpeningValue = openingValue;

                    await _assetTradingDataService.AddAsync(assetTrading);
                }                
            };
        }
    }
}
