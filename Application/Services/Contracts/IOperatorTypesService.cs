using RuleBuilderInfra.Application.PresentationModels;
using RuleBuilderInfra.Domain.Entities;

namespace RuleBuilderInfra.Application.Services.Contracts
{
    public interface IOperatorTypesService
    {
        Task<List<FieldOperatorJoiningModel>> GetOperatorTypesAsync(string fieldTypeCode);
    }
}
