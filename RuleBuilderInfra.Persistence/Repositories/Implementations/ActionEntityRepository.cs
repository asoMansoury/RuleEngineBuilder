using Microsoft.EntityFrameworkCore;
using RuleBuilderInfra.Domain.Entities;
using RuleBuilderInfra.Persistence.Repositories.Contracts;

namespace RuleBuilderInfra.Persistence.Repositories.Implementations
{
    public class ActionEntityRepository : BaseRepository, IActionEntityRepository
    {

        public ActionEntityRepository(RuleEngineContext ruleEngineContext) : base(ruleEngineContext)
        {
        }

        public async Task<ActionEntity> AddAsync(ActionEntity entity)
        {
            entity.IsActive = true;
            _dbContext.ActionEntities.Add(entity);
            return await Task.FromResult(entity);
        }

        public Task<List<ActionEntity>> EntitiesWhichNotAdded(List<ActionEntity> entities)
        {

            throw new NotImplementedException();
        }

        public async Task<ActionEntity> GetActionEntityById(long id)
        {
            return _dbContext.ActionEntities.Include(z => z.ActionPropertis).SingleOrDefault(z => z.Id == id && z.IsActive == true);
        }

        public async Task<ActionEntity> GetActionEntityByName(string actionName)
        {
            return _dbContext.ActionEntities.Include(z=>z.ActionPropertis).SingleOrDefault(z => z.ServiceName == actionName && z.IsActive == true);
        }

        public async Task<List<ActionEntity>> GetAllAsync()
        {
            var entities = await _dbContext.ActionEntities
                .Where(z => z.IsActive == true)
                .Include(item => item.ActionPropertis.Where(z => z.IsActive == true)).ToListAsync();

            return entities;
        }

        public async Task<bool> HasAnyAsync()
        {
            return await _dbContext.ActionEntities.AnyAsync();
        }

        public async Task<List<ActionEntity>> Where(Func<ActionEntity,bool> predicate)
        {
            var entities =  _dbContext.ActionEntities.Where(predicate).AsQueryable().Include(z=>z.ActionPropertis).ToList();  
            return entities; 
        }
    }

}
