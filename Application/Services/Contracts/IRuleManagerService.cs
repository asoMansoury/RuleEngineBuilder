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
        Task AddNewRuleAsync(RuleEntity ruleEntity);
        Task<IEnumerable<RuleEntity>> GetAllRulesAsync();
        Task<List<RuleEntity>> GetAll();
        Task<RuleEntity> GetRuleEntityByIdAsync(int id);
    }
}
