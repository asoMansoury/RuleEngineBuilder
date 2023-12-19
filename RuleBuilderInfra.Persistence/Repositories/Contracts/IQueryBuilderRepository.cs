namespace RuleBuilderInfra.Persistence.Repositories.Contracts
{
    public interface IQueryBuilderRepository<T>
    {
        List<T> GetDataGenericBuilder(Func<T, bool> predicate);
    }
}
