using RuleBuilderInfra.Application.PresentationModels;
using RuleBuilderInfra.Application.Services.Contracts;
using RuleBuilderInfra.Domain.Entities;
using RuleBuilderInfra.Persistence;
using RuleBuilderInfra.Persistence.Repositories.Contracts;
using System.Collections.Generic;

namespace RuleBuilderInfra.Application.Services.Implementations
{
    public class FakeDataService : BaseService, IFakeDataService
    {
        private readonly IFakeDataRepository _fakeDataRepository;
        public FakeDataService(IUnitOfWork unitOfWork, IFakeDataRepository fakeDataRepository) : base(unitOfWork)
        {
            this._fakeDataRepository = fakeDataRepository;
        }

        public async Task<List<FakeDataEntity>> GetDistencteMovieAsync()
        {
            return await  _fakeDataRepository.GetDistinctByMovies();
        }

        public async Task<List<FakeDataEntity>> GetDistencteProvincesAsync()
        {
            
            return await _fakeDataRepository.GetDistinctByProvinces();
        }

        public async Task<List<FakeDataEntity>> GetDistributerByMovieOrProvinceAsync(string province, string movie)
        {
            try
            {
                return await _fakeDataRepository.GetDistributerByMovieOrProvinceAsync(province, movie);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
