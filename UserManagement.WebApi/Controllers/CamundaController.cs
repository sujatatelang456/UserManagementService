using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserManagement.Application.Services;
using UserManagement.Domain.Entities;

namespace UserManagement.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CamundaController : ControllerBase
    {
        private readonly CamundaService _camundaService;
        public CamundaController(CamundaService camundaService) {
            _camundaService = camundaService;
        }

        [HttpPost("start/{processKey}")]
        public async Task<IActionResult> StartProcess(string processKey, Asset asset)
        {
            await _camundaService.StartProcess(processKey, asset);
            return Ok($"Process {processKey} started successfully with Invoice ID: {asset.AssetName}");
        }
    }
}
