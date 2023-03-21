using AssetChange.Domain.Dtos.External;
using AssetChange.Domain.Entities;

namespace AssetChange.Infra.Data.Repositories.Interfaces
{
    public interface IAssetRepository : IRepository<Asset>
    {
        public Task CreateAsync(YahooChartDto yahooChartDto);
    }
}
