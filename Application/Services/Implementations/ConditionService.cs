using RuleBuilderInfra.Application.PresentationModels;
using RuleBuilderInfra.Application.Services.Contracts;
using RuleBuilderInfra.Domain.Entities;
using RuleBuilderInfra.Persistence;
using RuleBuilderInfra.Persistence.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuleBuilderInfra.Application.Services.Implementations
{
    public class ConditionService : BaseService, IConditionService
    {
        private readonly IConditionRepository _conditionRepository;
        public ConditionService(IUnitOfWork unitOfWork,IConditionRepository conditionRepository) : base(unitOfWork)
        {
            _conditionRepository = conditionRepository;
        }

        public Task<List<ConditionEntity>> GetCondifitionEntityByCodeAsync()
        {
            return _conditionRepository.GetAllConditions();
        }

        public async Task<List<ConditionEntity>> GetConditionEntitiesAsync()
        {
            var conditionEntities = await _conditionRepository.GetAllConditions();
            return conditionEntities;
        }
    }
}
