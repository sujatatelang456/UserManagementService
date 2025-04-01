using UserManagement.Domain.Entities;
using UserManagement.Domain.Interfaces;

namespace UserManagement.Application.Services
{
    public class AssetService
    {
        private readonly IUnitOfWork _unitOfWork;
        // , IUserService userService
        public AssetService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task AddAsset(Asset asset)
        {
            await _unitOfWork.Assets.AddAsset(asset);
            await _unitOfWork.SaveChangesAsync();
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

        public async Task<Asset> GetAssetById(int id)
        {
            return await _unitOfWork.Assets.GetAssetById(id);
        }

        public async Task UpdateAsset(Asset asset)
        {
            await _unitOfWork.Assets.UpdateAsset(asset);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}