﻿using UserManagement.Domain.Interfaces;
using UserManagement.Infrastructure.Data;

namespace UserManagement.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        public IUserRepository Users { get; }
        public IAssetRepository Assets { get; }
        public IValuationTypeRepository valuationTypes { get; }
        public UnitOfWork(AppDbContext context, IUserRepository userRepository, IAssetRepository assetRepository, IValuationTypeRepository valuationTypesRepository)
        {
            _context = context;
            Users = userRepository;
            Assets = assetRepository;
            valuationTypes = valuationTypesRepository;  
        }
        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
