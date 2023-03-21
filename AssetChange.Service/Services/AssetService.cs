using AssetChange.Domain.Dtos.External;
using AssetChange.Domain.Entities;
using AssetChange.Infra.Data.Repositories.Interfaces;
using AssetChange.Service.Services.Interfaces;

namespace AssetChange.Service.Services
{
    public class AssetService : IAssetService
    {
        private readonly IAssetRepository _assetRepository;

        public AssetService(IAssetRepository assetRepository)
            => _assetRepository = assetRepository;

        public async Task AddAsync(Asset entity)
            => await _assetRepository.CreateAsync(entity);

        public async Task AddAsync(YahooChartDto yahooChartDto)
            => await _assetRepository.CreateAsync(yahooChartDto);

        public async Task<Asset> GetAsync(Asset entity)
            => await _assetRepository.DetailsAsync(entity);

        public async Task<IEnumerable<Asset>> GetMoreAsync(Asset entity)
            => await _assetRepository.RetrieveAsync(entity);

        public async Task RefreshAsync(Asset entity)
            => await _assetRepository.UpdateAsync(entity);

        public async Task RemoveAsync(Asset entity)
            => await _assetRepository.DeleteAsync(entity);
    }
}
