using Microsoft.EntityFrameworkCore;
using RuleBuilderInfra.Persistence.Repositories.Contracts;
using System.Linq;

namespace RuleBuilderInfra.Persistence.Repositories.Implementations
{
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
            try
            {
                return _dbContext.Set<T>().Where(predicate).ToList();
                
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
