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
        public async Task StartProcess(string clusterId, string processDefinitionId, AssetUploadRequest assetUploadRequest)
        {
            //HttpClient _httpClient = new HttpClient();
            //var accessToken = await GetAccessTokenByAudience("zeebe");

            var requestBody = new
            {
                processDefinitionId = processDefinitionId, //"template-human-task-tutorial-1pnmbd1",                
                variables = new
                {
                    propertyStatus = assetUploadRequest.PropertyStatus,
                    propertyPrice = assetUploadRequest.PropertyPrice,
                    dateAvailable = assetUploadRequest.DateAvailable.ToString("yyyy-MM-dd"), //dateAvailable.Replace("-","/"),
                    ownerName = assetUploadRequest.OwnerName,
                    ownerEmail = assetUploadRequest.OwnerEmail,
                    rework = assetUploadRequest.Rework,
                    timerDuration = assetUploadRequest.TimerDuration,
                    propertyAddress = assetUploadRequest.PropertyAddress,
                    ownerContact = assetUploadRequest.OwnerContact,
                    propertyType = assetUploadRequest.PropertyType,
                }
            };

            //// Convert object to JSON string
            //string jsonString = JsonSerializer.Serialize(requestBody);
            //// Create HttpContent with JSON payload
            //var content = new StringContent(jsonString, Encoding.UTF8, "application/json");
            //// Add headers (if needed)
            //_httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");
            // Send POST request
            // HttpResponseMessage response = await _httpClient.PostAsync($"https://syd-1.zeebe.camunda.io:443/{clusterId}/v2/process-instances", content);

            var url = $"https://syd-1.zeebe.camunda.io:443/{clusterId}/v2/process-instances";
            HttpResponseMessage response = await GetHttpResponseMessage(url, "zeebe", requestBody);

            // Read and display response
            string responseContent = await response.Content.ReadAsStringAsync();

            if (response.StatusCode == HttpStatusCode.OK) {
                HttpResponseMessage res = new HttpResponseMessage();
                // get Active task = taskState = CREATED
                var processRespose = JsonConvert.DeserializeObject<CamundaProcess>(responseContent);
                res = await GetProcessInstanceTasks(processRespose.processInstanceKey, clusterId);
                
                if (res.StatusCode == HttpStatusCode.OK) {
                    // assign task - TODO
                    // res = await AssignTask("", )
                }
            }
            else {
                // error handling 
            }
            Console.WriteLine($"Response: {response.StatusCode}");
            Console.WriteLine($"Body: {responseContent}");
        }

        public async Task<HttpResponseMessage> AssignTask(string taskId, string assignee, string clusterId)
        {
            var requestBody = new
            {
                assignee = assignee, //sujata.telang@infovision.com
                allowOverrideAssignment = true
            };

            var url = $"https://syd-1.tasklist.camunda.io/{clusterId}/v1/tasks/{taskId}/assign";
            HttpResponseMessage response = await GetHttpResponseMessage(url, "tasklist", requestBody);

            return response;
        }

        public async Task GetTaskDetails(string taskId, string clusterId)
        {
            var url = $"https://syd-1.tasklist.camunda.io/{clusterId}/v1/tasks/{taskId}";
            HttpResponseMessage response = await GetHttpResponseMessage(url, "tasklist", new object());
        }

        public async Task<HttpResponseMessage> GetProcessInstanceTasks(string processInstanceKey, string clusterId)
        {
            var requestBody = new
            {
                processInstanceKey = processInstanceKey
            };

            var url = $"https://syd-1.tasklist.camunda.io/{clusterId}/v1//tasks/search";
            HttpResponseMessage response = await GetHttpResponseMessage(url, "tasklist", requestBody);

            return response;
        }

        public async Task GetProcessInstanceVariables(string processInstanceKey, string clusterId)
        {
            var requestBody = new
            {
                processInstanceKey = processInstanceKey
            };

            var url = $"https://syd-1.operate.camunda.io/{clusterId}/v1/variables/search";
            HttpResponseMessage response = await GetHttpResponseMessage(url, "operate", requestBody);
        }

        public async Task SaveProcessVariables(string taskId, string clusterId, object variables)
        {
            var requestBody = new
            {
                variables = variables
            };

            var url = $"https://syd-1.operate.camunda.io/{clusterId}/v1/tasks/{taskId}/variables";
            HttpResponseMessage response = await GetHttpResponseMessage(url, "operate", requestBody);
        }

        private async Task<HttpResponseMessage> GetHttpResponseMessage(string url, string audience, object requestBody)
        {
            HttpClient _httpClient = new HttpClient();
            var accessToken = await GetAccessTokenByAudience(audience);

            // Convert object to JSON string
            string jsonString = JsonSerializer.Serialize(requestBody);

            // Create HttpContent with JSON payload
            var content = new StringContent(jsonString, Encoding.UTF8, "application/json");

            // Add headers (if needed)
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");

            // Send POST request
            HttpResponseMessage response = await _httpClient.PostAsync(url, content);

            return response;
        }
        public async Task<string> GetAccessTokenByAudience(string audience)
        {
            HttpClient _httpClient = new HttpClient();
            var values = new Dictionary<string, string>
            {
                { "grant_type", "client_credentials" },
                { "client_id", "zFey.6BmlPYuCbN-EnPlnRDud02cLh2E" },
                { "client_secret", "H8-qO152.MLIiA3jFaQIIgh-DR4pgr~Gi05Yopeu9As6Mno3yxFAEbN144ZEvABB" },
                { "audience", $"{audience}.camunda.io" }
            };

            var content = new FormUrlEncodedContent(values);
            var response = await _httpClient.PostAsync("https://login.cloud.camunda.io/oauth/token", content);
            var responseString = await response.Content.ReadAsStringAsync();

            var tokenResponse = JsonConvert.DeserializeObject<TokenResponse>(responseString);
            return tokenResponse.access_token;
        }
    }
}
