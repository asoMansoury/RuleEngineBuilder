using RuleBuilderInfra.Domain.Entities;

namespace RuleBuilderInfra.Persistence.Repositories.Contracts
{
    public interface IActionRuleEntityRepository
    {
        Task<ActionRuleEntity> AddActionRuleEntity(RuleEntity RuleEntity, ActionEntity actionEntity);
    }
}
