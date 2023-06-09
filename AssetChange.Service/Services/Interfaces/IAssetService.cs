﻿using AssetChange.Domain.Dtos;
using AssetChange.Domain.Dtos.External;
using AssetChange.Domain.Entities;

namespace AssetChange.Service.Services.Interfaces
{
    public interface IAssetService : IService<Asset>
    {
        public Task AddAsync(YahooChartDto yahooChartDto);
        public Task<List<AssetChangeDto>> GetMoreAssetChangeAsync(string assetName);
    }
}
