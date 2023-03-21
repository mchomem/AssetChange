using AssetChange.Domain.Entities;
using AssetChange.Infra.Data.Contexts;
using AssetChange.Infra.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AssetChange.Infra.Data.Repositories
{
    public class AssetTradingDateRepository : IAssetTradingDateRepository
    {
        private readonly AssetChangeContext _context;

        public AssetTradingDateRepository(AssetChangeContext context)
            => _context = context;

        public async Task CreateAsync(AssetTradingDate entity)
        {
            _context.Add(entity);

            _context.Asset.Attach(entity.Asset);
            _context.Entry(entity.Asset).State = EntityState.Unchanged;

            await _context.SaveChangesAsync();
        }

        public Task DeleteAsync(AssetTradingDate entity)
        {
            throw new NotImplementedException();
        }

        public Task<AssetTradingDate> DetailsAsync(AssetTradingDate entity)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<AssetTradingDate>> RetrieveAsync(AssetTradingDate entity)
        {
            if(entity != null)
                return await _context.AssetTradingDate
                    .Include(x => x.Asset)
                    .Where(x =>
                    (
                        (!entity.Id.HasValue || x.Id.Value == entity.Id.Value)
                        && (!entity.AssetId.HasValue || x.AssetId.Value == entity.AssetId.Value)
                        && (string.IsNullOrEmpty(entity.Asset.Symbol) || entity.Asset.Symbol.Contains(entity.Asset.Symbol))
                    ))
                    .ToListAsync();
            else
                return  await _context.AssetTradingDate.ToListAsync();
        }

        public Task UpdateAsync(AssetTradingDate entity)
        {
            throw new NotImplementedException();
        }
    }
}
