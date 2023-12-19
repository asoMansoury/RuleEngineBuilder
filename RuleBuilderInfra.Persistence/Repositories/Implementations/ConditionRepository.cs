using RuleBuilderInfra.Domain.Entities;
using RuleBuilderInfra.Persistence.Repositories.Contracts;

namespace RuleBuilderInfra.Persistence.Repositories.Implementations
{
    public class ConditionRepository : BaseRepository, IConditionRepository
    {
        public ConditionRepository(RuleEngineContext ruleEngineContext) : base(ruleEngineContext)
        {
        }

        public Task<List<ConditionEntity>> GetAllConditions()
        {
            try
            {
               return Task.FromResult(_dbContext.conditionEntities.ToList());
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public Task<List<ConditionEntity>> GetConditionsByCodes(string code)
        {
            try
            {
                return Task.FromResult(_dbContext.conditionEntities.Where(z=>z.Code == code).ToList());
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
