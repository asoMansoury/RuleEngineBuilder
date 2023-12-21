using Microsoft.EntityFrameworkCore;

namespace RuleBuilderInfra.Persistence.Repositories.Contracts
{
    public interface IQueryBuilderRepository<T>
    {
        List<T> GetDataGenericBuilder(Func<T, bool> predicate);
    }


    public interface IQueryBuilderRepositoryExternal<T, TContext> where T : class
                                                                  where TContext : DbContext
    {
        List<T> GetDataGenericBuilder(Func<T, bool> predicate);
    }
}
