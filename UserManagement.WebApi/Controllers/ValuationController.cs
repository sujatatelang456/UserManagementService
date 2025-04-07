using Microsoft.AspNetCore.Mvc;
using UserManagement.Application.Services;
using UserManagement.Domain.Entities;

namespace UserManagement.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuationController : ControllerBase
    {
        public readonly ValuationTypeService _valuationTypeService;

        public ValuationController(ValuationTypeService valuationTypeService)
        {
            _valuationTypeService = valuationTypeService;
        }
        [HttpGet]
        public async Task<IEnumerable<ValuationType>> GetAllValuationType()
        {
            return await _valuationTypeService.GetAllValuationTypesAsync();
        }
    }
}
