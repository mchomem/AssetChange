using AssetChange.Domain.Dtos.External;
using AssetChange.Domain.Entities;
using AssetChange.Infra.Data.Contexts;
using AssetChange.Infra.Data.Repositories.Interfaces;
using AssetChange.Infra.Data.Utils;
using Microsoft.EntityFrameworkCore;

namespace AssetChange.Infra.Data.Repositories
{
    public class AssetRepository : IAssetRepository
    {
        private readonly AssetChangeContext _context;

        public AssetRepository(AssetChangeContext context)
            => _context = context;

        public async Task CreateAsync(Asset entity)
        {
            _context.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task CreateAsync(YahooChartDto yahooChart)
        {
            var transaction = _context.Database.BeginTransaction();

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

                await CreateAsync(asset);

                for (int i = 0; i < item.Timestamp.Count; i++)
                {
                    DateTime? eventDate = CommonUtil.ConvertFromTimestamp(item.Timestamp[i]);
                    decimal? openingValue = item.IndicatorDto.IndicatorDtoQuote[0].Open[i];

                    AssetTradingDate assetTrading = new AssetTradingDate();
                    assetTrading.AssetId = asset.Id;
                    assetTrading.Asset = asset;
                    assetTrading.EventDate = eventDate;
                    assetTrading.OpeningValue = openingValue;

                    _context.Add(assetTrading);
                    _context.Asset.Attach(assetTrading.Asset);
                    _context.Entry(assetTrading.Asset).State = EntityState.Unchanged;

                    await _context.SaveChangesAsync();
                }
            };

            transaction.CommitAsync();
        }

        public async Task DeleteAsync(Asset entity)
        {
            _context.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<Asset?> DetailsAsync(Asset entity)
            => await _context.Asset.FirstOrDefaultAsync(x => x.Id == entity.Id);

        public async Task<IEnumerable<Asset>> RetrieveAsync(Asset entity)
        {
            if (entity != null)
                return await _context.Asset
                    .Where(x => (
                        (!entity.Id.HasValue || x.Id.Value == entity.Id.Value)
                        && (string.IsNullOrEmpty(entity.Symbol) || x.Symbol.Contains(entity.Symbol)) 
                    ))
                    .ToListAsync();
            else
                return await _context.Asset.ToListAsync();
        }

        public async Task UpdateAsync(Asset entity)
        {
            _context.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
