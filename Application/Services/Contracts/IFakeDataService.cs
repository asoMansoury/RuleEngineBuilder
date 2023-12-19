using RuleBuilderInfra.Application.PresentationModels;

namespace RuleBuilderInfra.Application.Services.Contracts
{
    public interface IFakeDataService
    {
        Task<List<FakeDataModel>> GetDistencteMovieAsync();
        Task<List<FakeDataModel>> GetDistencteProvincesAsync();
        Task<List<FakeDataModel>> GetDistributerByMovieOrProvinceAsync(string province,string movie);
    }
}
