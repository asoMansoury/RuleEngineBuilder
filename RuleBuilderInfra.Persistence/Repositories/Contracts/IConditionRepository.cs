using RuleBuilderInfra.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuleBuilderInfra.Persistence.Repositories.Contracts
{
    public interface IConditionRepository
    {
        Task<List<ConditionEntity>> GetAllConditions();
        Task<List<ConditionEntity>> GetConditionsByCodes(string code);
    }
}
