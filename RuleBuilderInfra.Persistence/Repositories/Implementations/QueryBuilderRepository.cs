using Microsoft.EntityFrameworkCore;
using RuleBuilderInfra.Persistence.Repositories.Contracts;

namespace RuleBuilderInfra.Persistence.Repositories.Implementations
{
    public class QueryBuilderRepository<T> : BaseRepository, IQueryBuilderRepository<T> where T : class
    {
        private readonly DbSet<T> _dbSet;
        public QueryBuilderRepository(RuleEngineContext ruleEngineContext) : base(ruleEngineContext)
        {
            _dbSet = _dbContext.Set<T>();
        }

        public List<T> GetDataGenericBuilder(Func<T, bool> predicate)
        {
            return _dbSet.Where(predicate).ToList();
        }
    }
}
