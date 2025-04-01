﻿using Microsoft.EntityFrameworkCore;
using UserManagement.Domain.Entities;
using UserManagement.Infrastructure.Data;

namespace UserManagement.Infrastructure.Repositories
{
    public class AssetRepository
    {
        private readonly AppDbContext _context;
        public AssetRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsset(Asset asset)
        {
            await _context.Assets.AddAsync(asset);
        }

        public async Task DeleteAsset(int id)
        {
            var asset = await _context.Assets.FindAsync(id);
            if (asset != null)
            {
                _context.Assets.Remove(asset);
            }
        }

        public async Task<IEnumerable<Asset>> GetAllAssets()
        {
            return await _context.Assets.ToArrayAsync();
        }

        public async Task<Asset> GetAssetById(int id)
        {
            return await _context.Assets.FindAsync(id);
        }

        public async Task UpdateAsset(Asset asset)
        {
            _context.Assets.Update(asset);
        }
    }
}