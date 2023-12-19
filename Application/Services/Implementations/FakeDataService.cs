using AutoMapper;
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
        public FakeDataService(IUnitOfWork unitOfWork, IMapper mapper, IFakeDataRepository fakeDataRepository) : base(unitOfWork,mapper)
        {
            this._fakeDataRepository = fakeDataRepository;
        }

        public async Task<List<FakeDataModel>> GetDistencteMovieAsync()
        {
            var distinctedObject =await  _fakeDataRepository.GetDistinctByMovies();
            return _mapper.Map<List<FakeDataModel>>(distinctedObject);
        }

        public async Task<List<FakeDataModel>> GetDistencteProvincesAsync()
        {
            
            var distinctedObject =await _fakeDataRepository.GetDistinctByProvinces();
            return _mapper.Map<List<FakeDataModel>>(distinctedObject);
        }

        public async Task<List<FakeDataModel>> GetDistributerByMovieOrProvinceAsync(string province, string movie)
        {
            try
            {
                var data = await _fakeDataRepository.GetDistributerByMovieOrProvinceAsync(province, movie);
                return _mapper.Map<List<FakeDataModel>>(data);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
