using Microsoft.EntityFrameworkCore;
using RuleBuilderInfra.Application.PresentationModels.RuleEngineModels;
using RuleBuilderInfra.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuleBuilderInfra.Application.Services.Contracts.RuleEngineer
{
    public interface IScanEntitiesEngineService<TContext> where TContext : DbContext
    {
        List<ScannableEntities> GetAllScannableEntities(string assemblyName);
        Task<List<RuleEngineProperties>> GetPropertyPairs(string assemblyName,string entityTypeCode);
        Task<RuleEngineProperties> GetTypeOfPropertyName(string assemblyName, string entityTypeCode,string propertyName);
        Task<object> GenerateQueryBuilder(RuleEntity ruleEntity);
    }
}
