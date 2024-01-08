using Microsoft.EntityFrameworkCore;
using RuleBuilderInfra.Domain.Entities;
using RuleBuilderInfra.Persistence.Repositories.Contracts;
using System.Linq;

namespace RuleBuilderInfra.Persistence.Repositories.Implementations
{
    public class ConditionRuleEntityRepository : BaseRepository, IConditionRuleEntityRepository
    {
        public ConditionRuleEntityRepository(RuleEngineContext ruleEngineContext) : base(ruleEngineContext)
        {

        }

        public async Task<ConditionRuleEntity> AddAsync(ConditionRuleEntity entity)
        {
            entity.IsActive = true;
            _dbContext.Add(entity);
            return entity;
        }

        public async Task<IQueryable<ConditionRuleEntity>> Where(Func<ConditionRuleEntity, bool> predicate)
        {
            var entities = _dbContext.ConditionRuleEntities.Where(predicate).AsQueryable();
            return entities;
        }

    }
}


