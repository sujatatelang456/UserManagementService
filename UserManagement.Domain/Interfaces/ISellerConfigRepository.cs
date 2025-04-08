using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Domain.Entities;

namespace UserManagement.Domain.Interfaces
{
    public interface ISellerConfigRepository
    {
        Task<SellerConfig> GetSellerConfigAsync();
        Task<SellerConfig> ToggleSellerConfig();
    }
}
