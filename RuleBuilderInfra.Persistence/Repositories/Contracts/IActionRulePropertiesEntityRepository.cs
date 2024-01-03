using RuleBuilderInfra.Domain.Entities;

namespace RuleBuilderInfra.Persistence.Repositories.Contracts
{
    public interface IActionRulePropertiesEntityRepository
    {
        Task<ActionRulePropertiesEntity> AddActionRuleEntity(RuleEntity RuleEntity, ActionPropertiesEntity Property,string value);
    }
}
