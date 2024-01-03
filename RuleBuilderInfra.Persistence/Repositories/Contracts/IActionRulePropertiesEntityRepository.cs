using RuleBuilderInfra.Domain.Entities;

namespace RuleBuilderInfra.Persistence.Repositories.Contracts
{
    public interface IActionRulePropertiesEntityRepository
    {
        Task<ActionRulePropertiesEntity> AddActionRuleEntity(ActionRuleEntity ActionRuleEntity, ActionPropertiesEntity Property,string value);
    }
}
