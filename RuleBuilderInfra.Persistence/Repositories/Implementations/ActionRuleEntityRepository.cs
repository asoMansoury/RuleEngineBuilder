using RuleBuilderInfra.Domain.Entities;
using RuleBuilderInfra.Persistence.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuleBuilderInfra.Persistence.Repositories.Implementations
{
    public class ActionRuleEntityRepository : BaseRepository, IActionRuleEntityRepository
    {
        public ActionRuleEntityRepository(RuleEngineContext ruleEngineContext) : base(ruleEngineContext)
        {
        }

        public async Task<ActionRuleEntity> AddActionRuleEntity(RuleEntity RuleEntity, ActionEntity actionEntity)
        {
            ActionRuleEntity entity = new ActionRuleEntity();
            entity.RuleEntity = RuleEntity;
            entity.ActionEntity = actionEntity;
            _dbContext.Set<ActionRuleEntity>().AddAsync(entity);
            return entity;
        }
    }

    
}
