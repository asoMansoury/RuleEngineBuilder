using RuleBuilderInfra.Domain.Entities;
using RuleBuilderInfra.Persistence.Repositories.Contracts;

namespace RuleBuilderInfra.Persistence.Repositories.Implementations
{
    public class ConditionRuleEntityRepository : BaseRepository, IConditionRuleEntityRepository
    {
        public ConditionRuleEntityRepository(RuleEngineContext ruleEngineContext) : base(ruleEngineContext)
        {

        }

        public async Task<ConditionRuleEntity> AddAsync(ConditionRuleEntity entity)
        {

            _dbContext.Add(entity);
            return entity;
        }

    }
}


