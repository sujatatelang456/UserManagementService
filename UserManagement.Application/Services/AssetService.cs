using UserManagement.Domain.Entities;
using UserManagement.Domain.Interfaces;
using Microsoft.Extensions.Options;

namespace UserManagement.Application.Services
{
    public class AssetService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly CamundaService _camundaService;
        private readonly string _connectionString;
        private readonly ServiceBusPublisher _publisher;

        // , IUserService userService
        public AssetService(IUnitOfWork unitOfWork, CamundaService camundaService, ServiceBusPublisher publisher)
        {
            _unitOfWork = unitOfWork;
            _camundaService = camundaService;
            _publisher = publisher;
        }

        public async Task AddAsset(Asset asset)
        {

            await _unitOfWork.Assets.AddAsset(asset);
            await _unitOfWork.SaveChangesAsync();

            //var getCamundaClusterId = _configuration["CamundaClusterID"];
            //await _camundaService.StartProcess(getCamundaClusterId, processDefinitionId, variables);
        }

        public async Task DeleteAsset(int id)
        {
            await _unitOfWork.Assets.DeleteAsset(id);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<Asset>> GetAllAssets()
        {
            return await _unitOfWork.Assets.GetAllAssets();
        }

        public async Task<Asset> GetAssetById(string assetId)
        {
            return await _unitOfWork.Assets.GetAssetById(assetId);
        }

        public async Task UpdateAsset(Asset asset)
        {
            await _unitOfWork.Assets.UpdateAsset(asset);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<dynamic> UpdateAssetStatus(string assetId, string assetStatus, string processInstanceKey)
        {
            await _unitOfWork.Assets.UpdateAssetStatus(assetId, assetStatus);
            await _unitOfWork.SaveChangesAsync();

            var asset = await _unitOfWork.Assets.GetAssetById(assetId);

            if (asset != null)
            {
                var updatedAsset = new { assetId = asset.AssetId, assetStatus = asset.AssetStatus, processInstanceKey = processInstanceKey };

                if (assetStatus.ToLower().Equals("cancel") || assetStatus.ToLower().Equals("hold"))
                {
                    string messageBody = $@"{{
                                          ""name"": ""remotebidding"",
                                          ""correlationKey"": ""{asset.AssetId}""
                                         }}";


                    await _publisher.SendMessageAsync(messageBody);
                }

                return updatedAsset;
            }

            return null;
        }

        public async Task TriggerExternalSystemEvent(string assetId, string eventName)
        {
            var messageBody = string.Empty;

            switch (eventName.ToLower())
            {
                case "sold":

                    messageBody = $@"{{
                                          ""name"": ""remotebidding"",
                                          ""correlationKey"": ""{assetId}"",
                                          ""variables"": {{
                                                            ""sold"": ""YES""
                                                         }}
                                         }}";
                    break;
                case "hold":
                    messageBody = $@"{{
                                          ""name"": ""holdOrCancel"",
                                          ""correlationKey"": ""{assetId}"",
                                          ""variables"": {{
                                                            ""hold"": ""true""
                                                         }}
                                         }}";
                    break;
                case "unhold":
                    messageBody = $@"{{
                                          ""name"": ""unhold"",
                                          ""correlationKey"": ""{assetId}""
                                         }}";
                    break;
                case "cancel":
                    messageBody = $@"{{
                                          ""name"": ""holdOrCancel"",
                                          ""correlationKey"": ""{assetId}"",
                                          ""variables"": {{
                                                            ""hold"": ""false""
                                                         }}
                                         }}";
                    break;
            }
                
            await _publisher.SendMessageAsync(messageBody);
        }

        public async Task TriggerExternalSystemEvent(string assetId)
        {
            string messageBody = $@"{{
                                          ""name"": ""remotebidding"",
                                          ""correlationKey"": ""{assetId}"",
                                          ""variables"": {{
                                                            ""sold"": ""YES""
                                                         }}
                                         }}";


            await _publisher.SendMessageAsync(messageBody);
        }
    }
}