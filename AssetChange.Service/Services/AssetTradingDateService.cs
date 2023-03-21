using AssetChange.Domain.Entities;
using AssetChange.Infra.Data.Repositories.Interfaces;
using AssetChange.Service.Services.Interfaces;

namespace AssetChange.Service.Services
{
    public class AssetTradingDateService : IAssetTradingDateService
    {
        private readonly IAssetTradingDateRepository _repository;

        public AssetTradingDateService(IAssetTradingDateRepository repository)
            => _repository = repository;

        public async Task AddAsync(AssetTradingDate entity)
            => await _repository.CreateAsync(entity);

        public Task<AssetTradingDate> GetAsync(AssetTradingDate entity)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<AssetTradingDate>> GetMoreAsync(AssetTradingDate entity)
        {
            throw new NotImplementedException();
        }

        public Task RefreshAsync(AssetTradingDate entity)
        {
            throw new NotImplementedException();
        }

        public Task RemoveAsync(AssetTradingDate entity)
        {
            throw new NotImplementedException();
        }
    }
}
