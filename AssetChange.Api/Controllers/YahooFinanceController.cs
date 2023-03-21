using AssetChange.Domain.Dtos.External;
using AssetChange.Service.Services.External;
using Microsoft.AspNetCore.Mvc;

namespace AssetChange.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class YahooFinanceController : ControllerBase
    {
        private readonly YahooFinanceService _yahooFinanceService;

        public YahooFinanceController(YahooFinanceService yahooFinanceService)
            => _yahooFinanceService = yahooFinanceService;

        /// <summary>
        /// Get asset data via Yahoo Finance external api.
        /// </summary>
        /// <param name="assetName">The name of the asset</param>
        /// <returns></returns>
        [HttpGet("asset")]
        public async Task<ActionResult<YahooChartDto?>> Get(string assetName = "PETR4.SA")
            => await _yahooFinanceService.GetFullData(assetName);

        /// <summary>
        /// Make a import of asset information.
        /// </summary>
        /// <param name="assetName">The name of asset</param>
        /// <returns></returns>
        [HttpPost("import")]
        public async Task PostImport(string assetName = "PETR4.SA")
            => await _yahooFinanceService.ImportData(assetName);
    }
}
