using RuleBuilderInfra.Application.PresentationModels;

namespace RuleBuilderInfra.Application.Services.Contracts
{
    public interface IOperatorTypesService
    {
        Task<List<OperatorTypesModel>> GetOperatorTypesAsync(string fieldTypeCode);
    }
}
