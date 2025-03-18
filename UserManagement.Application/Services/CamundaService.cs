using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using UserManagement.Domain.Entities;
//using System.Text.Json;
using System.Net;
using JsonSerializer = System.Text.Json.JsonSerializer;
using System.Runtime;
using System.Threading.Tasks;

namespace UserManagement.Application.Services
{
    public class CamundaService
    {
        public async Task<CamundaProcess> StartProcess(string clusterId, string processDefinitionId, AssetUploadRequest assetUploadRequest)
        {
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

            var url = $"https://syd-1.zeebe.camunda.io:443/{clusterId}/v2/process-instances";
            HttpResponseMessage response = await GetHttpResponseMessage(url, "zeebe", requestBody);

            // Read and display response
            string responseContent = await response.Content.ReadAsStringAsync();
            CamundaProcess processRespose = new CamundaProcess();
            processRespose = JsonConvert.DeserializeObject<CamundaProcess>(responseContent);
            // CamundaTask processTask= new CamundaTask();
            //if (response.StatusCode == HttpStatusCode.OK)
            //{
            // HttpResponseMessage res = new HttpResponseMessage();
            // get Active task = taskState = CREATED
            // var tasks = await GetProcessInstanceTasks(processRespose.processInstanceKey, clusterId);
            // var taskId = tasks.Any() ? tasks.FirstOrDefault(t => t.processInstanceKey == processRespose.processInstanceKey).id : null;

            //if (res.StatusCode == HttpStatusCode.OK)
            //{
            //    if (taskId != null)
            //    {
            //        processTask = await this.AssignCamundaTask(taskId, "kiran.patwardhan@infovision.com", clusterId);
            //    }
            //}
            //}
            //else
            //{
            //    // error handling 
            //}

            return processRespose;
        }

        public async Task<CamundaTask> AssignCamundaTask(string taskId, string assignee, string clusterId, string processInstanceKey)
        {
            CamundaTask task = new CamundaTask();
            var requestBody = new
            {
                assignee = assignee, //sujata.telang@infovision.com
                allowOverrideAssignment = true
            };
            
            var tasks = await GetProcessInstanceTasks(processInstanceKey, clusterId);
            var assigntask = tasks.Any() ? tasks.FirstOrDefault(t => t.id == taskId).id : string.Empty;

            if (assigntask != null)
            {
                var url = $"https://syd-1.tasklist.camunda.io/{clusterId}/v2/user-tasks/{taskId}/assignment";
                HttpResponseMessage response = await GetHttpResponseMessage(url, "tasklist", requestBody, "POST");

                string jsonString = await response.Content.ReadAsStringAsync();
                task = JsonConvert.DeserializeObject<CamundaTask>(jsonString);
            }
            return task;
        }

        public async Task GetTaskDetails(string taskId, string clusterId)
        {
            var url = $"https://syd-1.tasklist.camunda.io/{clusterId}/v1/tasks/{taskId}";
            HttpResponseMessage response = await GetHttpResponseMessage(url, "tasklist", new object());
        }

        public async Task<List<CamundaTask>> GetProcessInstanceTasks(string processInstanceKey, string clusterId)
        {
            var requestBody = new
            {
                processInstanceKey = processInstanceKey
            };

            var url = $"https://syd-1.tasklist.camunda.io/{clusterId}/v1/tasks/search";
            HttpResponseMessage response = await GetHttpResponseMessage(url, "tasklist", requestBody);

            string jsonString = await response.Content.ReadAsStringAsync();
            var tasks = JsonConvert.DeserializeObject<List<CamundaTask>>(jsonString) ?? new List<CamundaTask>();

            return tasks;
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

        public async Task<List<CamundaTaskVariable>> GetTaskVariables(string taskId, string clusterId)
        {
            var requestBody = new { };

            var url = $"https://syd-1.tasklist.camunda.io/{clusterId}/v1/tasks/{taskId}/variables/search";
            HttpResponseMessage response = await GetHttpResponseMessage(url, "tasklist", requestBody);

            string jsonString = await response.Content.ReadAsStringAsync();
            var variables = JsonConvert.DeserializeObject<List<CamundaTaskVariable>>(jsonString) ?? new List<CamundaTaskVariable>();

            return variables;
        }

        public async Task CompleteCamundaTask(string taskId, string clusterId, AssetUploadRequest variable)
        {
            // TODO - need to check how we can make this method generalized to make any task as complete
            // currently its tightly bound to asset upload task request object
            var requestBody = new
            {
                variables = new
                {
                    propertyStatus = variable.PropertyStatus,
                    propertyPrice = variable.PropertyPrice,
                    dateAvailable = variable.DateAvailable.ToString("yyyy-MM-dd"), //dateAvailable.Replace("-","/"),
                    ownerName = variable.OwnerName,
                    ownerEmail = variable.OwnerEmail,
                    rework = variable.Rework,
                    timerDuration = variable.TimerDuration,
                    propertyAddress = variable.PropertyAddress,
                    ownerContact = variable.OwnerContact,
                    propertyType = variable.PropertyType,
                }
            };

            var intTaskId = Convert.ToInt64(taskId);

            var url = $"https://syd-1.tasklist.camunda.io/{clusterId}/v2/user-tasks/{intTaskId}/completion";
           // HttpResponseMessage response = await GetHttpResponseMessage(url, "tasklist", requestBody);

            await GetHttpResponseMessage(url, "tasklist", requestBody);
            //string jsonString = await response.Content.ReadAsStringAsync();
            //var task = JsonConvert.DeserializeObject<CamundaTask>(jsonString) ?? new CamundaTask();

            //return response;
        }

        private async Task<HttpResponseMessage> GetHttpResponseMessage(string url, string audience, object requestBody, string method = "POST")
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
            HttpResponseMessage response = method == "POST" ? await _httpClient.PostAsync(url, content) : await _httpClient.PatchAsync(url, content);

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
