using RuleBuilderInfra.Domain.Entities;

namespace RuleBuilderInfra.Persistence.Repositories.Contracts
{
    public interface IActionPropertiesEntityRepository
    {
        Task AddAsync(ActionPropertiesEntity actionPropertiesEntity,ActionEntity parentEntity);
    }
}
