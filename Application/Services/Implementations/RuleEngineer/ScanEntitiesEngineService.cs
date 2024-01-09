using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using RuleBuilderInfra.Application.Mapping;
using RuleBuilderInfra.Application.PresentationModels;
using RuleBuilderInfra.Application.PresentationModels.RuleEngineModels;
using RuleBuilderInfra.Application.Services.Contracts;
using RuleBuilderInfra.Application.Services.Contracts.RuleEngineer;
using RuleBuilderInfra.Domain.Entities;
using RuleBuilderInfra.Domain.ScanningEntities;
using RuleBuilderInfra.Persistence;
using RuleBuilderInfra.Persistence.Repositories.Contracts;
using RuleBuilderInfra.Persistence.Repositories.Contracts.RuleEngine;
using RuleBuilderInfra.Persistence.Repositories.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace RuleBuilderInfra.Application.Services.Implementations.RuleEngineer
{
    public class ScanEntitiesEngineService<TContext> : IScanEntitiesEngineService<TContext> where TContext:DbContext
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IFieldTypesService _fieldTypesService;

        private readonly TContext _mainContext;

        public ScanEntitiesEngineService(IServiceProvider serviceProvider,
                                          IFieldTypesService fieldTypesService,
                                          RuleEngineContext ruleEngineContext,
                                          TContext mainDatabase)
        {
            _serviceProvider = serviceProvider;
            _fieldTypesService = fieldTypesService;
            _mainContext = mainDatabase as TContext;
        }

        private List<Type> LoadAllAssemblies(string assemblyName)
        {
            Assembly assm = Assembly.Load(assemblyName);
            return assm.GetTypes().Where(z => z.IsSubclassOf(typeof(ScannedEntity))).ToList();
        }

        private List<Type> LoadAllAssembliesByAttribute(string assemblyName)
        {
            Assembly assm = Assembly.Load(assemblyName);
            return assm.GetTypes().Where(z => z.GetCustomAttributes(typeof(ScanningAttribute)).Any(e => e.GetType() == typeof(ScanningAttribute))).ToList();
        }

        public List<ScannableEntities> GetAllScannableEntities(string assemblyName, CancellationToken cancellationToken)
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
                    Name = scannAttributeProperties.IsNameSet == true ? scannAttributeProperties.Name : item.Name
                });
            });

            return result;
        }

        public async Task<List<RuleEngineProperties>> GetPropertyPairs(string assemblyName, string entityTypeCode, CancellationToken cancellationToken)
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

        public async Task<RuleEngineProperties> GetTypeOfPropertyName(string assemblyName, string entityTypeCode, string propertyName, CancellationToken cancellationToken)
        {
            var propertyType = checkEntityScannedInstantiator(assemblyName, entityTypeCode).GetTypeOfPropertyName(propertyName);
            FieldTypesEntity fieldTypesCodes = await _fieldTypesService.GetFieldTypesByFieldType(propertyType);
            //var result = _mapper.Map<RuleEngineProperties>(fieldTypesCodes);
            var result = ManuallMapping.Map(fieldTypesCodes);
            result.PropertyName = propertyName;
            return result;
        }

        public async Task<object> GenerateQueryBuilder(RuleEntity ruleEntity, CancellationToken cancellationToken)
        {
            var queryBuilder = ruleQueryBuilderInstantiator(ruleEntity.EntityCategoryCode, ruleEntity.EntityCode);
            var generatedQuery = queryBuilder.GenerateQueryBuilder(ruleEntity);

            var queryRunner = queryBuilderRepositoryInstantiator(ruleEntity.EntityCategoryCode, ruleEntity.EntityCode);
            var result = queryRunner.GetDataGenericBuilder(generatedQuery);

            return result;
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



        private dynamic ruleQueryBuilderInstantiator(string assemblyName, string entityTypeCode)
        {
            Assembly assembly = Assembly.Load(assemblyName);

            Type tEntity = assembly!.GetType(entityTypeCode)!;
            Type tResultEntity = typeof(ScannedEntity);
            Type closedGenericType = typeof(IRuleBuilderEngineRepo<>).MakeGenericType(tEntity);
            var checkEntityScannedValidator = _serviceProvider.GetService(closedGenericType) as dynamic;
            return checkEntityScannedValidator!;
        }

        private dynamic queryBuilderRepositoryInstantiator(string assemblyName, string entityTypeCode)
        {
            Type tEntity = entityGenerator(assemblyName, entityTypeCode);
            Type mainDBContextType = _mainContext.GetType();

            // Create an instance of QueryBuilderRepositoryExternal using generics
            Type queryBuilderRepoType = typeof(QueryBuilderRepositoryExternal<,>);
            Type genericQueryBuilderRepoType = queryBuilderRepoType.MakeGenericType(tEntity, mainDBContextType);
            object[] constructorArgs = { _mainContext };  // Pass _mainContext to the constructor
            object runner = Activator.CreateInstance(genericQueryBuilderRepoType, constructorArgs)!;
            return runner!;
        }

        private Type entityGenerator(string assemblyName, string entityTypeCode)
        {
            Assembly assembly = Assembly.Load(assemblyName);

            Type tEntity = assembly!.GetType(entityTypeCode)!;
            return tEntity;
        }
        #endregion
    }
}
