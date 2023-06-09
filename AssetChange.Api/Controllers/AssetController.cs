﻿using AssetChange.Domain.Dtos;
using AssetChange.Domain.Entities;
using AssetChange.Service.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AssetChange.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssetController : ControllerBase
    {
        private readonly IAssetService _assetService;

        public AssetController(IAssetService assetService)
            => _assetService = assetService;

        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<ActionResult<IEnumerable<Asset>>> Get()
        {
            try
            {
                return Ok(await _assetService.GetMoreAsync(null));
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        /// <summary>
        /// Get an analytical report on the price variation of an asset.
        /// </summary>
        /// <param name="assetName">The name of asset</param>
        /// <returns></returns>
        [HttpGet("price-change")]
        public async Task<ActionResult<List<AssetChangeDto>>> GetAssetChange(string assetName = "PETR4.SA")
        {
            try
            {
                return Ok(await _assetService.GetMoreAssetChangeAsync(assetName));
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet("{id}")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<ActionResult<Asset>> Get(int id)
        {
            try
            {
                return Ok(await _assetService.GetAsync(new Asset() { Id = id }));
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPost]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<ActionResult> Post(Asset asset)
        {
            try
            {
                await _assetService.AddAsync(asset);

                return Ok(new { Message = "Asset created"});
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPut("{id}")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<ActionResult> Put(Asset asset)
        {
            try
            {
                await _assetService.RefreshAsync(asset);

                return Ok(new { Message = "Asset updated." });
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpDelete("{id}")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _assetService.RemoveAsync(new Asset() { Id = id});

                return Ok(new { Message = "Asset removed." });
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}
