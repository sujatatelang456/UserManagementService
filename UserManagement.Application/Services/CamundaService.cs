using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using UserManagement.Domain.Entities;
//using System.Text.Json;
using System.Net;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace UserManagement.Application.Services
{
    public class CamundaService
    {       

        public async Task StartProcess(string clusterId)
        {
            HttpClient _httpClient = new HttpClient();
            var accessToken = await GetOperateAccessToken();
            var requestBody = new
            {
                processDefinitionId = "template-human-task-tutorial-1pnmbd1",
            };

            // Convert object to JSON string
            string jsonString = JsonSerializer.Serialize(requestBody);

            // Create HttpContent with JSON payload
            var content = new StringContent(jsonString, Encoding.UTF8, "application/json");

            // Add headers (if needed)
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");

            // Send POST request
            HttpResponseMessage response = await _httpClient.PostAsync($"https://syd-1.zeebe.camunda.io:443/{clusterId}/v2/process-instances", content);

            // Read and display response
            string responseContent = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Response: {response.StatusCode}");
            Console.WriteLine($"Body: {responseContent}");            
        }

        public async Task<string> GetOperateAccessToken()
        {
            HttpClient _httpClient = new HttpClient();
            var values = new Dictionary<string, string>
            {
                { "grant_type", "client_credentials" },
                { "client_id", "zFey.6BmlPYuCbN-EnPlnRDud02cLh2E" },
                { "client_secret", "H8-qO152.MLIiA3jFaQIIgh-DR4pgr~Gi05Yopeu9As6Mno3yxFAEbN144ZEvABB" },
                { "audience", "zeebe.camunda.io" }
            };

            var content = new FormUrlEncodedContent(values);
            var response = await _httpClient.PostAsync("https://login.cloud.camunda.io/oauth/token", content);
            var responseString = await response.Content.ReadAsStringAsync();

            var tokenResponse = JsonConvert.DeserializeObject<TokenResponse>(responseString);
            return tokenResponse.access_token;
        }

    }
}
