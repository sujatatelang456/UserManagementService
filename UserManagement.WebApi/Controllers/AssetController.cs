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

        [HttpGet("{id}")]
        public async Task<Asset> GetAssetById(int id)
        {
            return await _assetService.GetAssetById(id);
        }

        [HttpPost]
        public async Task<IActionResult> AddAsset(Asset asset)
        {
            await _assetService.AddAsset(asset);
            return CreatedAtAction(nameof(GetAssetById), new { id = asset.Id }, asset);
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
    }
}