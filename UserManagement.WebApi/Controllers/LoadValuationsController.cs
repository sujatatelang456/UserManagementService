using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserManagement.Application.Services;
using UserManagement.Domain.Entities;

namespace UserManagement.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoadValuationsController : ControllerBase
    {
        public readonly LoadValuationService _loadValuationService;

        public LoadValuationsController(LoadValuationService loadValuationService)
        {
            _loadValuationService = loadValuationService;
        }

        [HttpGet]
        public async Task<LoadValuation> GetAllLoadValuations()
        {
            return await _loadValuationService.GetXVMAsync();
        }   
    }
}
