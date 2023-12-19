using AutoMapper;
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
        public ConditionService(IUnitOfWork unitOfWork,IMapper mapper,IConditionRepository conditionRepository) : base(unitOfWork,mapper)
        {
            _conditionRepository = conditionRepository;
        }

        public Task<List<ConditionModel>> GetCondifitionEntityByCodeAsync()
        {
            var conditionEntities = _conditionRepository.GetAllConditions();
            return Task.FromResult(base._mapper.Map<List<ConditionModel>>(conditionEntities));
        }

        public async Task<List<ConditionModel>> GetConditionEntitiesAsync()
        {
            var conditionEntities = await _conditionRepository.GetAllConditions();
            var result = base._mapper.Map<List<ConditionModel>>(conditionEntities);
            return result;
        }
    }
}
