using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Domain.Entities;
using UserManagement.Domain.Interfaces;

namespace UserManagement.Infrastructure.Repositories
{
    public class LoadValuationRepository : ILoadValuationRepository
    {
        public Task<LoadValuation> GetXVMAsync()
        {
            return Task.FromResult(
                new LoadValuation { Id = 1, AssetId = "A7", ValuationEffectiveDate = "06/05/2024", AsIsValue = "$880580", ValuationType = "Automated Valuation Model" }
            );
        }
    }
}
