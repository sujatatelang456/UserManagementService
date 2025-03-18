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
        private readonly IConfiguration _configuration;
        
        public CamundaController(CamundaService camundaService, IConfiguration configuration) {
            _camundaService = camundaService;
            _configuration = configuration;
        }

        //[HttpPost("start/{processKey}")]
        //public async Task<IActionResult> StartProcess(string processKey, Asset asset)
        //{
        //    await _camundaService.StartProcess(processKey, asset);
        //    return Ok($"Process {processKey} started successfully with Invoice ID: {asset.AssetName}");
        //}
        [HttpPost("StartProcess")]
        public async Task<CamundaProcess> StartProcess(string processDefinitionId, AssetUploadRequest assetUploadRequest)
        {
            var getCamundaClusterId = _configuration["CamundaClusterID"];
            return await _camundaService.StartProcess(getCamundaClusterId, processDefinitionId,assetUploadRequest);
            // return Ok($"Process started successfully");
        }
        [HttpPost("AssignCamundaTask")]
        public async Task<CamundaTask> AssignCamundaTask(string taskId, string assignee, string processInstanceKey)
        {
            var getCamundaClusterId = _configuration["CamundaClusterID"];
            return await _camundaService.AssignCamundaTask(taskId, assignee, getCamundaClusterId, processInstanceKey);
            // return Ok($"Process started successfully");
        }
        [HttpPost("CompleteCamundaTask")]
        public async Task<CamundaTask> CompleteCamundaTask(string taskId, object variables)
        {
            var getCamundaClusterId = _configuration["CamundaClusterID"];
            return await _camundaService.CompleteCamundaTask(taskId, getCamundaClusterId, variables);
            // return Ok($"Process started successfully");
        }
    }
}
