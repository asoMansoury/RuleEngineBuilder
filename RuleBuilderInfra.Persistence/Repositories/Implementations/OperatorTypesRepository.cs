using RuleBuilderInfra.Persistence.Repositories.Contracts;

namespace RuleBuilderInfra.Persistence.Repositories.Implementations
{
    public class OperatorTypesRepository : BaseRepository, IOperatorTypesRepository
    {
        public OperatorTypesRepository(RuleEngineContext ruleEngineContext) : base(ruleEngineContext)
        {
        }
    }
}
