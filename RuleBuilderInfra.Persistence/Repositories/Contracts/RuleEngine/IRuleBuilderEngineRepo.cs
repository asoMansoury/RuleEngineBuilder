using RuleBuilderInfra.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuleBuilderInfra.Persistence.Repositories.Contracts.RuleEngine
{
    public interface IRuleBuilderEngineRepo<T>
    {
        Func<T, bool> GenerateQueryBuilder(RuleEntity ruleEntity);
    }
}
