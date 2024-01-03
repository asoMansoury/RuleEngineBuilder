using RuleBuilderInfra.Domain.Entities;

namespace RuleBuilderInfra.Persistence.Repositories.Contracts
{
    public interface IActionEntityRepository
    {
        Task<ActionEntity> AddAsync(ActionEntity entity);
        Task<List<ActionEntity>> GetAllAsync();
        Task<bool> HasAnyAsync();

        Task<List<ActionEntity>> EntitiesWhichNotAdded(List<ActionEntity> entities);

        Task<List<ActionEntity>> Where(Func<ActionEntity, bool> predicate);
        Task<ActionEntity> GetActionEntityByName(string actionName);
        Task<ActionEntity> GetActionEntityById(Int64 id);
    }
}
