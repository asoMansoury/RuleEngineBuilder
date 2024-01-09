using RuleBuilderInfra.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuleBuilderInfra.Persistence.Repositories.Contracts
{
    public interface IRuleEntityRepository
    {
        Task<RuleEntity> AddAsync(RuleEntity entity);
        Task<List<RuleEntity>> GetAllAsync();
        Task<RuleEntity> GetRuleEntityByIdAsync(Int64 id);
        Task<RuleEntity> GetRuleEntityByQueryExpression(string queryExpression);
        Task<RuleEntity> GetRuleEntityByComparingObject(List<ConditionRuleEntity> conditionRuleEntities);
        Task<IQueryable<RuleEntity>> Where(Func<RuleEntity, bool> predicate);
        Task<RuleEntity> DeleteRule(RuleEntity entity);
    }
}
