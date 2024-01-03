using RuleBuilderInfra.Application.PresentationModels;

namespace RuleBuilderInfra.Application.Services.Contracts
{
    public interface IActionService
    {
        Task InsertScannedActions();
        Task<List<BusinessServiceModel>> GetActions();
        Task UpdateActions();

       

    }
}
