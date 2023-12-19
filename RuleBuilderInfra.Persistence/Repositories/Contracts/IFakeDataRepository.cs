using RuleBuilderInfra.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuleBuilderInfra.Persistence.Repositories.Contracts
{
    public interface IFakeDataRepository
    {
        List<FakeDataEntity> GetDataEntitiesBy(Func<FakeDataEntity, bool> predicate);

        Task<List<FakeDataEntity>> GetDistinctByMovies();

        Task< List<FakeDataEntity>> GetDistinctByProvinces();
        Task<List<FakeDataEntity>> GetDistributerByMovieOrProvinceAsync(string province, string movie);
    }
}
