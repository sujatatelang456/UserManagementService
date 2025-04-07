using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserManagement.Application.Services;
using UserManagement.Domain.Entities;

namespace UserManagement.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SellerConfigController : ControllerBase
    {
        public readonly SellerConfigService _sellerConfigService;

        public SellerConfigController(SellerConfigService sellerConfigService)
        {
            _sellerConfigService = sellerConfigService;
        }

        [HttpGet]
        public async Task<SellerConfig> GetAllSellerConfigs()
        {
            return await _sellerConfigService.GetSellerConfigAsync();
        }
    }
}
