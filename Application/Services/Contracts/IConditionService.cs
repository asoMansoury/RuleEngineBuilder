using RuleBuilderInfra.Application.PresentationModels;
using RuleBuilderInfra.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuleBuilderInfra.Application.Services.Contracts
{
    public interface IConditionService
    {
        Task<List<ConditionEntity>> GetConditionEntitiesAsync();
        Task<List<ConditionEntity>> GetCondifitionEntityByCodeAsync();
    }
}
