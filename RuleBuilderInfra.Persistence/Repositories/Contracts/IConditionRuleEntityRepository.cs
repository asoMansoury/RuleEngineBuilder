using RuleBuilderInfra.Domain.Entities;

namespace RuleBuilderInfra.Persistence.Repositories.Contracts
{
    public interface IConditionRuleEntityRepository
    {
        Task<ConditionRuleEntity> AddAsync(ConditionRuleEntity entity);
    }
}
