using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Domain.Entities;
using UserManagement.Domain.Interfaces;

namespace UserManagement.Application.Services
{
    public class LoadValuationService
    {
        private readonly IUnitOfWork _unitOfWork;
        public LoadValuationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<LoadValuation> GetXVMAsync()
        {
            var loadValuation = await _unitOfWork.LoadValuations.GetXVMAsync();
            return loadValuation;
        }   
    }
}
