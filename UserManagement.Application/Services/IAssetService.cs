using UserManagement.Domain.Entities;

namespace UserManagement.Application.Services
{
    public interface IAssetService
    {
        Task<IEnumerable<Asset>> GetAllAssets();
        Task<Asset> GetAssetById(int id);
        Task AddAsset(Asset asset);
        Task UpdateAsset(Asset asset);
        Task DeleteAsset(int id);

    }
}