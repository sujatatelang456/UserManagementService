using UserManagement.Domain.Entities;
using UserManagement.Domain.Interfaces;

namespace UserManagement.Application.Services
{
    public class AssetService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly CamundaService _camundaService;

        // , IUserService userService
        public AssetService(IUnitOfWork unitOfWork, CamundaService camundaService)
        {
            _unitOfWork = unitOfWork;
            _camundaService = camundaService;
        }

        public async Task AddAsset(Asset asset)
        {

            await _unitOfWork.Assets.AddAsset(asset);
            await _unitOfWork.SaveChangesAsync();

            //var getCamundaClusterId = _configuration["CamundaClusterID"];
            //await _camundaService.StartProcess(getCamundaClusterId, processDefinitionId, variables);
        }

        public async Task DeleteAsset(int id)
        {
            await _unitOfWork.Assets.DeleteAsset(id);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<Asset>> GetAllAssets()
        {
            return await _unitOfWork.Assets.GetAllAssets();
        }

        public async Task<Asset> GetAssetById(string assetId)
        {
            return await _unitOfWork.Assets.GetAssetById(assetId);
        }

        public async Task UpdateAsset(Asset asset)
        {
            await _unitOfWork.Assets.UpdateAsset(asset);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}