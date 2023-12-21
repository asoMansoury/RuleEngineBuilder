using AutoMapper;
using Microsoft.VisualBasic.FileIO;
using RuleBuilderInfra.Application.BuisinessModel;
using RuleBuilderInfra.Application.PresentationModels;
using RuleBuilderInfra.Application.PresentationModels.RuleEngineModels;
using RuleBuilderInfra.Application.Services.Contracts;
using RuleBuilderInfra.Application.Services.Contracts.RuleEngineer;
using RuleBuilderInfra.Domain.Entities;
using RuleBuilderInfra.Persistence;
using RuleBuilderInfra.Persistence.Repositories.Contracts;
using RuleBuilderInfra.Persistence.Repositories.Contracts.RuleEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RuleBuilderInfra.Application.Services.Implementations.RuleEngineer
{
    public class RuleBuilderEngineService<T, TResult> : BaseService, IRuleBuilderEngineService<T, TResult>
        where TResult : ScannedEntity
        where T : ScannedEntity
    {
        private readonly IRuleBuilderEngineRepo<T> _ruleBuilderEngineRepo;
        private readonly IFakeDataRepository _fakeDataRepository;
        private readonly IQueryBuilderRepository<T> _queryBuilderRepository;
        private readonly ICheckEntityIsScanned<TResult> _checkEntityIsScannedValidator;
        private readonly IFieldTypesService _fieldTypesService;
        public RuleBuilderEngineService(IUnitOfWork unitOfWork, IMapper mapper,
                                        IRuleBuilderEngineRepo<T> ruleBuilderEngineRepo,
                                        IFakeDataRepository fakeDataRepository,
                                        IQueryBuilderRepository<T> queryBuilderRepository,
                                        IFieldTypesService fieldTypesService,
                                        ICheckEntityIsScanned<TResult> checkEntityIsScannedValidator) : base(unitOfWork, mapper)
        {
            _ruleBuilderEngineRepo = ruleBuilderEngineRepo;
            _fakeDataRepository = fakeDataRepository;
            _queryBuilderRepository = queryBuilderRepository;
            _checkEntityIsScannedValidator = checkEntityIsScannedValidator;
            _fieldTypesService = fieldTypesService;
        }

        public List<TResult> GenerateQueryBuilder(RuleEntity ruleEntity)
        {
            var query = _ruleBuilderEngineRepo.GenerateQueryBuilder(ruleEntity);
            var data = _queryBuilderRepository.GetDataGenericBuilder(query);
            return _mapper.Map<List<T>, List<TResult>>(data);
        }

        public Func<T, bool> GenerateQueryBuilderQuery(RuleEntity ruleEntity)
        {
            return _ruleBuilderEngineRepo.GenerateQueryBuilder(ruleEntity);
        }

        public async Task<List<RuleEngineProperties>> GetPropertyPairs()
        {
            var result = new List<RuleEngineProperties>();
            var propertiesDictionary = _checkEntityIsScannedValidator.GetPropertyPairs();
            List<string> fieldTypes = propertiesDictionary.Select(z => z.Value).GroupBy(z => z).Select(z => z.Key.ToString()).ToList();
            var fieldTypesCodes = await _fieldTypesService.GetFieldTypesByFieldType(fieldTypes);
            foreach (var item in propertiesDictionary)
            {
                result.Add(new RuleEngineProperties
                {
                    PropertyName = item.Key,
                    FieldTypeCode = fieldTypesCodes.SingleOrDefault(z => z.FieldType == item.Value).FieldTypeCode,
                    FieldType = item.Value
                });
            }
            return result;
        }

        public async Task<RuleEngineProperties> GetTypeOfPropertyName(string propertyName)
        {
            var propertyType = _checkEntityIsScannedValidator.GetTypeOfPropertyName(propertyName);
            var fieldTypesCodes = await _fieldTypesService.GetFieldTypesByFieldType(propertyType);
            var result = _mapper.Map<RuleEngineProperties>(fieldTypesCodes);
            result.PropertyName = propertyName;
            return result;
        }

    }
}
