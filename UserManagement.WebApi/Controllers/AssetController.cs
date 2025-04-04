using Microsoft.AspNetCore.Mvc;
using UserManagement.Application.Services;
using UserManagement.Domain.Entities;

namespace UserManagement.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssetController : ControllerBase
    {
        public readonly AssetService _assetService;
        public AssetController(AssetService assetService)
        {
            _assetService = assetService;
        }
        [HttpGet]
        public async Task<IEnumerable<Asset>> GetAllAssets()
        {
            return await _assetService.GetAllAssets();
        }

        [HttpGet("{assetId}")]
        public async Task<Asset> GetAssetById(string assetId)
        {
            return await _assetService.GetAssetById(assetId);
        }

        [HttpPost]
        public async Task<IActionResult> AddAsset(Asset asset)
        {
            await _assetService.AddAsset(asset);
            return CreatedAtAction(nameof(GetAssetById), new { assetId = asset.AssetId}, asset);
        }

        [HttpPut]
        public async Task UpdateAsset(Asset asset)
        {
            await _assetService.UpdateAsset(asset);
        }

        [HttpDelete("{id}")]
        public async Task DeleteAsset(int id)
        {
            await _assetService.DeleteAsset(id);
        }

        [HttpPut("UpdateAssetStatus")]
        public async Task<dynamic> UpdateAssetStatus(string assetId, string assetStatus, string processInstanceKey)
        {
            var asset = await _assetService.UpdateAssetStatus(assetId, assetStatus, processInstanceKey);
            return asset;
        }
    }
}