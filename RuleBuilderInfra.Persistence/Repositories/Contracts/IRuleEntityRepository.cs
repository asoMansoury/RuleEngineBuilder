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
        Task<RuleEntity> GetRuleEntityByIdAsync(int id);
    }
}
