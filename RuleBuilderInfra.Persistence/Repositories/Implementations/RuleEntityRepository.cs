using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
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
            var entities = _dbContext.ruleEntities.Include(item => item.ConditionRulesEntity).ToList();
            entities.ForEach(item =>
            {
                item.Conditions = item.ConditionRulesEntity.Select(item => new ConditionRuleEntity
                {
                    ConditionCode = item.ConditionCode,
                    Id = item.Id,
                    Conditions = item.Conditions,
                    Operator = item.Operator,
                    Parent = item.Parent,
                    ParentId = item.ParentId,
                    PropertyName = item.PropertyName,
                    Value = item.Value
                }).ToList();
            });
            return entities;
        }

        public async Task<RuleEntity> GetRuleEntityByIdAsync(int id)
        {
            var entity = _dbContext.ruleEntities.Include(item => item.ConditionRulesEntity).SingleOrDefault(z => z.Id == id)!;
            entity.Conditions = entity.ConditionRulesEntity;
            return _dbContext.ruleEntities.Include(item => item.ConditionRulesEntity).SingleOrDefault(z => z.Id == id)!;
        }
    }
}


