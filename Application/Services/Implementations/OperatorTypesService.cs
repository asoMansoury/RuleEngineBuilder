using RuleBuilderInfra.Application.Mapping;
using RuleBuilderInfra.Application.PresentationModels;
using RuleBuilderInfra.Application.Services.Contracts;
using RuleBuilderInfra.Domain.Entities;
using RuleBuilderInfra.Persistence;
using RuleBuilderInfra.Persistence.Repositories.Contracts;
using System.Collections.Generic;

namespace RuleBuilderInfra.Application.Services.Implementations
{
    public class OperatorTypesService : BaseService, IOperatorTypesService
    {
        private readonly IOperatorTypesRepository _operatorTypesRepository;
        private readonly IFieldOperatorJoiningRepository _fieldOperatorJoiningRepository;
        public OperatorTypesService(IUnitOfWork unitOfWork,

                                    IOperatorTypesRepository operatorTypesRepository,
                                    IFieldOperatorJoiningRepository fieldOperatorJoiningRepository) : base(unitOfWork)
        {
            this._operatorTypesRepository = operatorTypesRepository;
            _fieldOperatorJoiningRepository = fieldOperatorJoiningRepository;
        }

        public async Task<List<FieldOperatorJoiningModel>> GetOperatorTypesAsync(string fieldTypeCode)
        {
            var data = await _fieldOperatorJoiningRepository.GetFieldOperatorByCode(fieldTypeCode);
            return ManuallMapping.Map(data);
        }
    }
}
