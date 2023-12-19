using AutoMapper;
using Newtonsoft.Json;
using RuleBuilderInfra.Application.PresentationModels;
using RuleBuilderInfra.Application.PresentationModels.RuleEngineModels;
using RuleBuilderInfra.Application.Services.Contracts;
using RuleBuilderInfra.Application.Services.Contracts.RuleEngineer;
using RuleBuilderInfra.Domain.Entities;
using RuleBuilderInfra.Domain.ScanningEntities;
using RuleBuilderInfra.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace RuleBuilderInfra.Application.Services.Implementations.RuleEngineer
{
    public class ScanEntitiesEngineService : IScanEntitiesEngineService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IFieldTypesService _fieldTypesService;
        private readonly IMapper _mapper;
        public ScanEntitiesEngineService(IServiceProvider serviceProvider,
                                          IFieldTypesService fieldTypesService,
                                          IMapper mapper)
        {
            _serviceProvider = serviceProvider;
            _fieldTypesService = fieldTypesService;
            _mapper = mapper;
        }

        private List<Type> LoadAllAssemblies(string assemblyName)
        {
            Assembly assm = Assembly.Load(assemblyName);
            return assm.GetTypes().Where(z => z.IsSubclassOf(typeof(ScannedEntity))).ToList();
        }

        private List<Type> LoadAllAssembliesByAttribute(string assemblyName)
        {
            Assembly assm = Assembly.Load(assemblyName);
            return assm.GetTypes().Where(z => z.GetCustomAttributes(typeof(ScanningAttribute)).Any(e=>e.GetType()== typeof(ScanningAttribute))).ToList();
        }

        public List<ScannableEntities> GetAllScannableEntities(string assemblyName)
        {
            var result = new List<ScannableEntities>();

            LoadAllAssembliesByAttribute(assemblyName).ForEach(item =>
            {
                var scannAttributeProperties = (item.GetCustomAttributes(typeof(ScanningAttribute), true).FirstOrDefault() as ScanningAttribute);
                var assemblyQualifiedName = item?.AssemblyQualifiedName?.Split(",")[0];

                result.Add(new ScannableEntities()
                {
                    EntityCode = assemblyQualifiedName,
                    EntityAssembly = assemblyQualifiedName,
                    Description = scannAttributeProperties.EntityDescription,
                    Name = scannAttributeProperties.IsNameSet==true? scannAttributeProperties.Name:item.Name
                });
            });

            return result;
        }

        public async Task<List<RuleEngineProperties>> GetPropertyPairs(string assemblyName, string entityTypeCode)
        {
            var result = new List<RuleEngineProperties>();

            var propertiesDictionary = checkEntityScannedInstantiator(assemblyName, entityTypeCode).GetPropertyPairs() as Dictionary<string, string>;
            //Load all properties 

            List<string> fieldTypes = propertiesDictionary.Select(z => z.Value).GroupBy(z => z).Select(z => z.Key.ToString()).ToList();
            var fieldTypesCodes = await _fieldTypesService.GetFieldTypesByFieldType(fieldTypes);
            foreach (var item in propertiesDictionary)
            {
                if (!fieldTypesCodes.Any(z => z.FieldType == item.Value))
                    throw new ArgumentOutOfRangeException();
                result.Add(new RuleEngineProperties
                {
                    PropertyName = item.Key,
                    FieldTypeCode = fieldTypesCodes.SingleOrDefault(z => z.FieldType == item.Value).FieldTypeCode,
                    FieldType = item.Value
                });
            }
            return result;
        }

        public async Task<RuleEngineProperties> GetTypeOfPropertyName(string assemblyName, string entityTypeCode, string propertyName)
        {
            var propertyType = checkEntityScannedInstantiator(assemblyName, entityTypeCode).GetTypeOfPropertyName(propertyName);
            var fieldTypesCodes = await _fieldTypesService.GetFieldTypesByFieldType(propertyType);
            var result = _mapper.Map<RuleEngineProperties>(fieldTypesCodes);
            result.PropertyName = propertyName;
            return result;
        }

        public async Task<object> GenerateQueryBuilder(RuleEntity ruleEntity)
        {
            var obj = ruleBuilderInstantiator(ruleEntity.EntityCategoryCode, ruleEntity.EntityCode);
            var data = obj.GenerateQueryBuilder(ruleEntity);
            return data;
        }



        #region Private Methods
        //In this method, Firsty find entityType code type and then asked MakeGenericType to generate our ICheckedEntityBased our entity Type and then we generated an instance from our CheckEntityIsScannedValidator
        private dynamic checkEntityScannedInstantiator(string assemblyName, string entityTypeCode)
        {
            Assembly assembly = Assembly.Load(assemblyName);
            Type entityType = CommonUtility.LoadAssemblyType(assemblyName, entityTypeCode);
            Type closedGenericType = typeof(ICheckEntityIsScanned<>).MakeGenericType(entityType);
            var checkEntityScannedValidator = _serviceProvider.GetService(closedGenericType) as dynamic;
            return checkEntityScannedValidator;
        }

        private dynamic ruleBuilderInstantiator(string assemblyName, string entityTypeCode)
        {
            Assembly assembly = Assembly.Load(assemblyName);

            Type tEntity = assembly.GetType(entityTypeCode); // Replace with your actual type
            Type tResultEntity = typeof(ScannedEntity);
            Type closedGenericType = typeof(IRuleBuilderEngineService<,>).MakeGenericType(tEntity, tResultEntity);
            var checkEntityScannedValidator = _serviceProvider.GetService(closedGenericType) as dynamic;
            return checkEntityScannedValidator;
        }
        #endregion
    }
}
