using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Domain.Entities;
using UserManagement.Domain.Interfaces;

namespace UserManagement.Infrastructure.Repositories
{
    public class SellerConfigRepository: ISellerConfigRepository
    {
        public Task<SellerConfig> GetSellerConfigAsync()
        {
            return Task.FromResult(new SellerConfig
            {
                Id = 1,
                FundTracking = "Funds Tracking",
                Status = true
            });
        }
    }    
}
