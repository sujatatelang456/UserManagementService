﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Domain.Entities;

namespace UserManagement.Domain.Interfaces
{
    public interface IValuationTypeRepository
    {
        Task<List<ValuationType>> GetAllValuationTypesAsync();
    }
}
