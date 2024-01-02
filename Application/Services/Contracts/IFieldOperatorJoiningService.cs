using RuleBuilderInfra.Application.PresentationModels;
using RuleBuilderInfra.Domain.Entities;

namespace RuleBuilderInfra.Application.Services.Contracts
{
    public interface IFieldOperatorJoiningService
    {
        Task<List<FieldOperatorJoiningEntity>> GetFieldOperatorByCode(string code);
    }
}
