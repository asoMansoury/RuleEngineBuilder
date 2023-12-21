using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using RuleBuilderInfra.Domain.Entities;
using RuleBuilderInfra.Persistence.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuleBuilderInfra.Persistence.Repositories.Implementations
{
    public class RuleEntityRepository : BaseRepository, IRuleEntityRepository
    {
        public RuleEntityRepository(RuleEngineContext ruleEngineContext) : base(ruleEngineContext)
        {

        }

        public async Task<RuleEntity> AddAsync(RuleEntity entity)
        {
            entity.Id = 0;
            _dbContext.Add(entity);
            return entity;
        }

        public async Task<List<RuleEntity>> GetAllAsync()
        {
            var entities = _dbContext.ruleEntities.ToList().ToList();
            return entities;
        }

        public async Task<RuleEntity> GetRuleEntityByIdAsync(int id)
        {
            return _dbContext.ruleEntities.SingleOrDefault(z => z.Id == id)!;
        }
    }
}
