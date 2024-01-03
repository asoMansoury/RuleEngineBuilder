using RuleBuilderInfra.Domain.Entities;
using RuleBuilderInfra.Persistence.Repositories.Contracts;

namespace RuleBuilderInfra.Persistence.Repositories.Implementations
{
    public class ActionRulePropertiesEntityRepository : BaseRepository, IActionRulePropertiesEntityRepository
    {
        public ActionRulePropertiesEntityRepository(RuleEngineContext ruleEngineContext) : base(ruleEngineContext)
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

        public async Task<ActionRulePropertiesEntity> AddActionRuleEntity(RuleEntity RuleEntity, ActionPropertiesEntity Property, string value)
        {
            ActionRulePropertiesEntity entity = new ActionRulePropertiesEntity();
            entity.RuleEntity = RuleEntity;
            entity.ActionPropertyEntity = Property;
            entity.Value = value;
            _dbContext.Set<ActionRulePropertiesEntity>().AddAsync(entity);
            return entity;
        }
    }

    
}
