using RuleBuilderInfra.Application.BuisinessModel;
using RuleBuilderInfra.Application.PresentationModels;
using RuleBuilderInfra.Application.PresentationModels.RuleEngineModels;
using RuleBuilderInfra.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuleBuilderInfra.Application.Services.Contracts.RuleEngineer
{
    public interface IRuleBuilderEngineService<T, TResult> where TResult : ScannedEntity
                                                           where T : ScannedEntity
    {
        Func<T, bool> GenerateQueryBuilderQuery(RuleEntity ruleEntity);
        List<TResult> GenerateQueryBuilder(RuleEntity ruleEntity);

        Task<List<RuleEngineProperties>> GetPropertyPairs();

        Task<RuleEngineProperties> GetTypeOfPropertyName(string propertyName);
    }
}
