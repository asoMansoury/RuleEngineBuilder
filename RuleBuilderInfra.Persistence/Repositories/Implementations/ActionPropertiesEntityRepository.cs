using RuleBuilderInfra.Domain.Entities;
using RuleBuilderInfra.Persistence.Repositories.Contracts;

namespace RuleBuilderInfra.Persistence.Repositories.Implementations
{
    public class ActionPropertiesEntityRepository : BaseRepository, IActionPropertiesEntityRepository
    {
        public ActionPropertiesEntityRepository(RuleEngineContext ruleEngineContext) : base(ruleEngineContext)
        {
        }

        public async Task AddAsync(ActionPropertiesEntity actionPropertiesEntity, ActionEntity parentEntity)
        {
            actionPropertiesEntity.ActionEntity = parentEntity;
            _dbContext.actionPropertisEntities.Add(actionPropertiesEntity);
        }
    }
    
}
