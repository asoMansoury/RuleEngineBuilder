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
        List<ScannableEntities> GetAllScannableEntities(string assemblyName, CancellationToken cancellationToken);
        Task<List<RuleEngineProperties>> GetPropertyPairs(string assemblyName,string entityTypeCode, CancellationToken cancellationToken);
        Task<RuleEngineProperties> GetTypeOfPropertyName(string assemblyName, string entityTypeCode,string propertyName, CancellationToken cancellationToken);
        Task<object> GenerateQueryBuilder(RuleEntity ruleEntity, CancellationToken cancellationToken);
    }
}
