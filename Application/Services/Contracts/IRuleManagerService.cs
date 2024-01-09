using RuleBuilderInfra.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuleBuilderInfra.Application.Services.Contracts
{
    public interface IRuleManagerService
    {
        Task AddNewRuleAsync(RuleEntity ruleEntity, CancellationToken cancellationToken);
        Task<IEnumerable<RuleEntity>> GetAllRulesAsync( CancellationToken cancellationToken);
        Task<List<RuleEntity>> GetAll(CancellationToken cancellationToken);
        Task<RuleEntity> GetRuleEntityByIdAsync(Int64 id, CancellationToken cancellationToken);
        Task<RuleEntity> FindConditionRuleEntity(List<ConditionRuleEntity> entities, CancellationToken cancellationToken);
        Task<string> GetOrderedExpressiont(string queryExpression, CancellationToken cancellationToken);

        Task<bool> DeleteRule(Int64 ruleEntityID, CancellationToken cancellationToken);
    }
}
