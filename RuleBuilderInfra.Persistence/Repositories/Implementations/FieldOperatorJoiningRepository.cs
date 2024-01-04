using Microsoft.EntityFrameworkCore;
using RuleBuilderInfra.Domain.Entities;
using RuleBuilderInfra.Persistence.Repositories.Contracts;

namespace RuleBuilderInfra.Persistence.Repositories.Implementations
{
    public class FieldOperatorJoiningRepository : BaseRepository, IFieldOperatorJoiningRepository
    {
        public FieldOperatorJoiningRepository(RuleEngineContext ruleEngineContext) : base(ruleEngineContext)
        {
        }

        public async Task<List<FieldOperatorJoiningEntity>> GetFieldOperatorByCode(string code)
        {
            try
            {
                return base._dbContext.FieldOperatorJoiningEntities.Where(z => z.FieldTypeCode == code)
                                                                   .Include(z=>z.OperatorTypesEntity)
                                                                   .ToList();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
