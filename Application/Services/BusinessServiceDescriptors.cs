using Newtonsoft.Json;
using RuleBuilderInfra.Application.PresentationModels.BuisinessEngineModels;
using RuleBuilderInfra.Application.Services.Contracts;
using RuleBuilderInfra.Application.Services.Contracts.RuleEngineer;
using RuleBuilderInfra.Domain.Entities;
using System.Reflection;

namespace RuleBuilderInfra.Application.Services
{
    public class BusinessServiceDescriptors : ICallingBusinessServiceMediator
    {
        private readonly ICategoryManagerService _assemblyManagerService;
        private readonly IRuleManagerService _ruleManagerService;
        private readonly IScanEntitiesEngineService _scanEntitiesEngineService;

        public BusinessServiceDescriptors(ICategoryManagerService assemblyManagerService,
                                            IRuleManagerService ruleManagerService,
                                            IScanEntitiesEngineService scanEntitiesEngineService)
        {
            _assemblyManagerService = assemblyManagerService;
            _ruleManagerService = ruleManagerService;
            _scanEntitiesEngineService = scanEntitiesEngineService;
        }

        //public void InvokeAsync<TOutputSearch, TInuptBusiness>(string categoryService, string serviceName, string outputSearchJson, TInuptBusiness inuptBusiness)
        //                    where TOutputSearch : ScannedEntity
        //                    where TInuptBusiness : BusinessEngineModel
        //{
        //    object foundServiceInstance = _assemblyManagerService.GetSelectedServiceType(categoryService, serviceName);
        //    var inputBusinessModel = _assemblyManagerService.GetInuptBusiness(categoryService, serviceName);
        //    var businessEngine = (BusinessEngine<TInuptBusiness>)foundServiceInstance;
        //    businessEngine.InvokeAsync(outputSearchJson, inuptBusiness);
        //}

        public void InvokeAsync(string categoryService, string serviceName, string outputSearchJson, string inputParamsJson, params object[] objects) 
        {
            object foundServiceInstance = _assemblyManagerService.GetSelectedServiceType(categoryService, serviceName);
            Type inputBusinessModel = _assemblyManagerService.GetInuptBusiness(categoryService, serviceName);
            var inputModel = JsonConvert.DeserializeObject(inputParamsJson, inputBusinessModel);
            var businessEngine = Activator.CreateInstance(foundServiceInstance.GetType());

            MethodInfo performAsyncLogicMethod = foundServiceInstance.GetType().GetMethod("PerformAsyncLogic", BindingFlags.NonPublic | BindingFlags.Instance);
            Task performAsyncLogicTask = (Task)performAsyncLogicMethod.Invoke(businessEngine, new[] { outputSearchJson, inputModel, objects });

        }

        public async Task<object> InvokeAsync(int ruleEntityId, params object[] objects) 
        {
            try
            {
                var ruleEntity = await _ruleManagerService.GetRuleEntityByIdAsync(ruleEntityId);
                var outputSearchJson = await this._scanEntitiesEngineService.GenerateQueryBuilder(ruleEntity);

                object foundServiceInstance = _assemblyManagerService.GetSelectedServiceType(ruleEntity.CategoryService, ruleEntity.ServiceName);
                Type inputBusinessModel = _assemblyManagerService.GetInuptBusiness(ruleEntity.CategoryService, ruleEntity.ServiceName);
                var inputModel = JsonConvert.DeserializeObject(ruleEntity.JsonValue, inputBusinessModel);
                var businessEngine = Activator.CreateInstance(foundServiceInstance.GetType());

                MethodInfo performAsyncLogicMethod = foundServiceInstance.GetType().GetMethod("PerformAsyncLogic", BindingFlags.NonPublic | BindingFlags.Instance);

                var performAsyncLogicTask = (Task<object>)performAsyncLogicMethod.Invoke(businessEngine, new object[] { JsonConvert.SerializeObject(outputSearchJson), inputModel, objects });
                return await performAsyncLogicTask;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void InvokeFromDatabaseAsync(string categoryService, string serviceName, string outputSearchJson, string inputParamsJson) 
        {


        }
    }
}
