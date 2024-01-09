using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using RuleBuilderInfra.Application.PresentationModels.BuisinessEngineModels;
using RuleBuilderInfra.Application.Services.Contracts;
using RuleBuilderInfra.Application.Services.Contracts.RuleEngineer;
using RuleBuilderInfra.Domain.Entities;
using RuleBuilderInfra.Persistence;
using System.Diagnostics;
using System.Reflection;

namespace RuleBuilderInfra.Application.Services
{
    public class BusinessServiceDescriptors<TContext> : ICallingBusinessServiceMediator<TContext> where TContext : DbContext
    {
        private readonly ICategoryManagerService _assemblyManagerService;
        private readonly IRuleManagerService _ruleManagerService;
        private readonly IScanEntitiesEngineService<TContext> _scanEntitiesEngineService;

        public BusinessServiceDescriptors(ICategoryManagerService assemblyManagerService,
                                            IRuleManagerService ruleManagerService,
                                            IScanEntitiesEngineService<TContext> scanEntitiesEngineService)
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

        public async Task<object> InvokeAsync(Int64 ruleEntityId, CancellationToken cancellationToken, params object[] objects)
        {
            object result = null;
            try
            {
                var ruleEntity = await _ruleManagerService.GetRuleEntityByIdAsync(ruleEntityId, cancellationToken);
                var outputSearchJson = await this._scanEntitiesEngineService.GenerateQueryBuilder(ruleEntity,cancellationToken);
                foreach (var actionRuleItem in ruleEntity.actionRuleEntities!)
                {
                    object foundServiceInstance1 = _assemblyManagerService.GetSelectedServiceType(actionRuleItem.ActionEntity.CategoryService, actionRuleItem.ActionEntity.ServiceName);
                    Type inputBusinessModel1 = _assemblyManagerService.GetInuptBusiness(actionRuleItem.ActionEntity.CategoryService, actionRuleItem.ActionEntity.ServiceName);
                    object instance = Activator.CreateInstance(inputBusinessModel1);
                    foreach (var actionPropertyItem in actionRuleItem.actionRulePropertiesEntities)
                    {
                        var selectedProperty = inputBusinessModel1.GetProperties().SingleOrDefault(item => item.Name == actionPropertyItem.ActionPropertyEntity.PropertyName);

                        string assemblyName = "mscorlib";
                        Assembly assembly = Assembly.Load(assemblyName);
                        string typeName = $"System.{actionPropertyItem.ActionPropertyEntity.PropertyType}";
                        Type targetType = assembly.GetType(typeName);
                        var value = Convert.ChangeType(actionPropertyItem.Value, targetType);

                        inputBusinessModel1.GetProperty(actionPropertyItem.ActionPropertyEntity.PropertyName)!.SetValue(instance, value);
                    }

                    var businessEngine1 = Activator.CreateInstance(foundServiceInstance1.GetType());

                    MethodInfo performAsyncLogicMethod1 = foundServiceInstance1.GetType().GetMethod("PerformAsyncLogic", BindingFlags.NonPublic | BindingFlags.Instance);

                    var performAsyncLogicTas1k = (Task<object>)performAsyncLogicMethod1.Invoke(businessEngine1, new object[] { JsonConvert.SerializeObject(outputSearchJson), instance, objects });
                    result = await performAsyncLogicTas1k;
                }
                return result;




                //object foundServiceInstance = _assemblyManagerService.GetSelectedServiceType(ruleEntity.CategoryService, ruleEntity.ServiceName);
                //Type inputBusinessModel = _assemblyManagerService.GetInuptBusiness(ruleEntity.CategoryService, ruleEntity.ServiceName);
                //var inputModel = JsonConvert.DeserializeObject(ruleEntity.JsonValue, inputBusinessModel);
                //var businessEngine = Activator.CreateInstance(foundServiceInstance.GetType());

                //MethodInfo performAsyncLogicMethod = foundServiceInstance.GetType().GetMethod("PerformAsyncLogic", BindingFlags.NonPublic | BindingFlags.Instance);

                //var performAsyncLogicTask = (Task<object>)performAsyncLogicMethod.Invoke(businessEngine, new object[] { JsonConvert.SerializeObject(outputSearchJson), inputModel, objects });
                //return await performAsyncLogicTask;
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
