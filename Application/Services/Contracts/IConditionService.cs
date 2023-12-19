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
        Task<List<ConditionModel>> GetConditionEntitiesAsync();
        Task<List<ConditionModel>> GetCondifitionEntityByCodeAsync();
    }
}
