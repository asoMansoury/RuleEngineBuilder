using RuleBuilderInfra.Domain.Entities;

namespace RuleBuilderInfra.Persistence.Repositories.Contracts
{
    public interface IFieldOperatorJoiningRepository
    {
        Task<List<FieldOperatorJoiningEntity>> GetFieldOperatorByCode(string fieldTypeCode);
    }
}
