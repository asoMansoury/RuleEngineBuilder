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

    public class QueryBuilderRepositoryExternal<T,TContext> : IQueryBuilderRepositoryExternal<T, TContext> where T : class
                                                                                                 where TContext :  DbContext
    {
        private readonly DbContext _dbContext;
        public QueryBuilderRepositoryExternal(DbContext dbContext) 
        {
            _dbContext = dbContext as TContext;
        }

        public List<T> GetDataGenericBuilder(Func<T, bool> predicate)
        {
            return _dbContext.Set<T>().Where(predicate).ToList();
        }
    }
}
