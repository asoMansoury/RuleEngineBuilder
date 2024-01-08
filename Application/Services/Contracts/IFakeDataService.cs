using RuleBuilderInfra.Application.PresentationModels;
using RuleBuilderInfra.Application.PresentationModels.BuisinessEngineModels;
using RuleBuilderInfra.Domain.Entities;

namespace RuleBuilderInfra.Application.Services.Contracts
{
    public interface IFakeDataService
    {
        Task<List<FakeDataEntity>> GetDistencteMovieAsync();
        Task<List<FakeDataEntity>> GetDistencteProvincesAsync();
        Task<List<FakeDataEntity>> GetDistributerByMovieOrProvinceAsync(string province,string movie);
        Task<TaxCalculatorForFakeModelResponse> CallTaxServices(FakeDataModelSample ruleEntity, int EarnedAmount, CancellationToken cancellationToken);
    }
}
