﻿using UserManagement.Domain.Interfaces;

namespace UserManagement.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository Users { get; }
        IAssetRepository Assets { get; }
        IValuationTypeRepository valuationTypes { get; }
        ILoadValuationRepository LoadValuations { get; }
        ISellerConfigRepository SellerConfig { get; }
        Task<int> SaveChangesAsync();
    }
}
