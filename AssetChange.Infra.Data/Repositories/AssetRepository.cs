using AssetChange.Domain.Entities;
using AssetChange.Infra.Data.Contexts;
using AssetChange.Infra.Data.Repositories.Interfaces;
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
