using AssetChange.Domain.Dtos;
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

                await _context.CurrentTradingPeriod.AddAsync(new CurrentTradingPeriod()
                {
                    AssetId = asset.Id,
                    Asset = asset,
                    Timezone = item.AssetDto.CurrentTradingPeriod.CurrentTradingPeriodPre.Timezone,
                    Start = CommonUtil.ConvertFromTimestamp(item.AssetDto.CurrentTradingPeriod.CurrentTradingPeriodPre.Start),
                    End = CommonUtil.ConvertFromTimestamp(item.AssetDto.CurrentTradingPeriod.CurrentTradingPeriodPre.End),
                    Gmtoffset = item.AssetDto.CurrentTradingPeriod.CurrentTradingPeriodPre.Gmtoffset,
                    Type = CurrentTradingPeriodType.PRE
                });

                await _context.SaveChangesAsync();

                await _context.CurrentTradingPeriod.AddAsync(new CurrentTradingPeriod()
                {
                    AssetId = asset.Id,
                    Asset = asset,
                    Timezone = item.AssetDto.CurrentTradingPeriod.CurrentTradingPeriodRegular.Timezone,
                    Start = CommonUtil.ConvertFromTimestamp(item.AssetDto.CurrentTradingPeriod.CurrentTradingPeriodRegular.Start),
                    End = CommonUtil.ConvertFromTimestamp(item.AssetDto.CurrentTradingPeriod.CurrentTradingPeriodRegular.End),
                    Gmtoffset = item.AssetDto.CurrentTradingPeriod.CurrentTradingPeriodRegular.Gmtoffset,
                    Type = CurrentTradingPeriodType.REGULAR
                });

                await _context.SaveChangesAsync();

                await _context.CurrentTradingPeriod.AddAsync(new CurrentTradingPeriod()
                {
                    AssetId = asset.Id,
                    Asset = asset,
                    Timezone = item.AssetDto.CurrentTradingPeriod.CurrentTradingPeriodPost.Timezone,
                    Start = CommonUtil.ConvertFromTimestamp(item.AssetDto.CurrentTradingPeriod.CurrentTradingPeriodPost.Start),
                    End = CommonUtil.ConvertFromTimestamp(item.AssetDto.CurrentTradingPeriod.CurrentTradingPeriodPost.End),
                    Gmtoffset = item.AssetDto.CurrentTradingPeriod.CurrentTradingPeriodPost.Gmtoffset,
                    Type = CurrentTradingPeriodType.POST
                });

                await _context.SaveChangesAsync();

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

        public async Task<List<AssetChangeDto>> RetreaveAssetChangeAsync(string assetName)
        {
            // Get first importation from Asset.
            Asset asset = await _context.Asset
                .Where(x => x.Symbol == assetName)
                .OrderByDescending(x => x.ImportedIn)
                .FirstOrDefaultAsync();

            return await _context.AssetTradingDate
                .Where(x => x.AssetId == asset.Id)
                .OrderBy(x => x.Id)
                .Select(x => new AssetChangeDto()
                {
                    Id = x.Id,
                    EventData = x.EventDate,
                    OpeningValue = x.OpeningValue,
                    PercentageD1 = BusinessUtil.CalculateAndFormatPriceChange(x.OpeningValue, _context.AssetTradingDate.OrderBy(i => i.Id).Where(i => i.Id < x.Id && i.AssetId == asset.Id).Select(i => i.OpeningValue).LastOrDefault()),
                    PercentageFirstDay = BusinessUtil.CalculateAndFormatPriceChange(x.OpeningValue, _context.AssetTradingDate.Where(y => y.AssetId == asset.Id).OrderBy(y => y.Id).Select(i => i.OpeningValue).FirstOrDefault())
                })
                .OrderByDescending(x => x.Id)
                .Take(30)
                .ToListAsync();
        }

        public async Task UpdateAsync(Asset entity)
        {
            _context.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
