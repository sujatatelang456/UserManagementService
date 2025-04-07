using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Domain.Entities;
using UserManagement.Domain.Interfaces;

namespace UserManagement.Infrastructure.Repositories
{
    public class ValuationTypeRepository : IValuationTypeRepository
    {
        public Task<List<ValuationType>> GetAllValuationTypesAsync()
        {
            // Simulate async database call
            return Task.FromResult(new List<ValuationType>
            {
                new ValuationType { Id = 1, ValuationTypeName = "Unassigned" },
                new ValuationType { Id = 2, ValuationTypeName = "ListingAgent" },
                new ValuationType { Id = 3, ValuationTypeName = "Appraisal" },
                new ValuationType { Id = 4, ValuationTypeName = "Supplemental" },
                new ValuationType { Id = 5, ValuationTypeName = "Drive By" },
                new ValuationType { Id = 6, ValuationTypeName = "ListingAgent Updated" },
                new ValuationType { Id = 7, ValuationTypeName = "Land - Only BPO" },
                new ValuationType { Id = 8, ValuationTypeName = "Automated Valuation Model" },
                new ValuationType { Id = 9, ValuationTypeName = "Land Only" },
                new ValuationType { Id = 10, ValuationTypeName = "Listing Agent BPO" },
                new ValuationType { Id = 11, ValuationTypeName = "Listing Agent Updated" },
            });
        }
    }
}
