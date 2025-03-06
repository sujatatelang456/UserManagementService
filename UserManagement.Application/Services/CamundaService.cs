using Newtonsoft.Json;
using System.Text;
using UserManagement.Domain.Entities;

namespace UserManagement.Application.Services
{
    public class CamundaService
    {
        private readonly HttpClient _httpClient;
        public CamundaService(HttpClient httpClient) {
            _httpClient = httpClient;
        }

        public async Task StartProcess(string processKey, Asset asset)
        {
            var requestBody = new
            {
                variables = new
                {
                    assetId = new { value = asset.AssetId, type = "integer" },
                    assetName = new { value = asset.AssetName, type = "String" },
                    assetAddress = new { value = asset.AssetAddress, type = "String" }
                }
            };

            var content = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"http://localhost:8080/engine-rest/process-definition/key/{processKey}/start", content);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Process Started: {result}");
        }  
    }
}
