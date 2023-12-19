using RuleBuilderInfra.Application.PresentationModels;

namespace RuleBuilderInfra.Application.Services.Contracts
{
    public interface IFieldOperatorJoiningService
    {
        Task<List<FieldOperatorJoiningModel>> GetFieldOperatorByCode(string code);
    }
}
