using Microsoft.EntityFrameworkCore;
using RuleBuilderInfra.Domain.Entities;
using RuleBuilderInfra.Persistence.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuleBuilderInfra.Persistence.Repositories.Implementations
{
    public class FakeDataRepository : MainBaseRepository, IFakeDataRepository
    {
        public FakeDataRepository(MainDatabase mainDbContext) : base(mainDbContext)
        {
        }

        public List<FakeDataEntity> GetDataEntitiesBy(Func<FakeDataEntity, bool> predicate)
        {
            return base._dbContext.fakeDataEntities.Where(predicate).ToList();
        }

        //public async Task<List<FakeDataEntity>> GetDistinctByMovies()
        //{
        //    return await base._dbContext.fakeDataEntities
        //        .GroupBy(z => z.Movie)
        //        .Select(g => g.First())
        //        .ToListAsync();
        //}

        public async Task<List<FakeDataEntity>> GetDistinctByMovies()
        {
            return await base._dbContext.fakeDataEntities
                .Select(z => new FakeDataEntity() { Movie = z.Movie })
                .Distinct()
                .ToListAsync();
        }

        public async Task<List<FakeDataEntity>> GetDistinctByProvinces()
        {
            return await base._dbContext.fakeDataEntities.Select(z => new FakeDataEntity() { Province = z.Province }).Distinct().ToListAsync();
        }

        public async Task<List<FakeDataEntity>> GetDistributerByMovieOrProvinceAsync(string province, string movie)
        {
            if (!string.IsNullOrEmpty(province) && string.IsNullOrEmpty(movie))
                return  base._dbContext.fakeDataEntities.Where(z => z.Province.Equals(province)).ToList();
            if (string.IsNullOrEmpty(province) && !string.IsNullOrEmpty(movie))
                return  base._dbContext.fakeDataEntities.Where(z => z.Movie.Equals(movie)).ToList();
            if (!string.IsNullOrEmpty(province) && !string.IsNullOrEmpty(movie))
                return  base._dbContext.fakeDataEntities.Where(z => z.Movie.Equals(movie) && z.Province.Equals(province)).ToList();
            throw new NotImplementedException();
        }
    }
}
