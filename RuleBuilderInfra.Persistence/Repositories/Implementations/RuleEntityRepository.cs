using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Newtonsoft.Json;
using RuleBuilderInfra.Domain.Entities;
using RuleBuilderInfra.Persistence.Repositories.Contracts;
using RuleBuilderInfra.Persistence.Repositories.Implementations.RuleEntityService;
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
            entity.IsActive = true;
            SetRuleEntityToChilds(entity.Conditions, entity);
            _dbContext.Add(entity);
            return entity;
        }

        public async void SetRuleEntityToChilds(List<ConditionRuleEntity> conditionRuleEntities, RuleEntity ruleEntity)
        {
            foreach (var conditionRuleEntity in conditionRuleEntities)
            {
                conditionRuleEntity.RuleEntity = ruleEntity;
                if (conditionRuleEntity.Conditions.Count > 0)
                    SetRuleEntityToChilds(conditionRuleEntity.Conditions, ruleEntity);
            }
        }


        public async Task<List<RuleEntity>> GetAllAsync()
        {
            var entities = _dbContext.RuleEntities
                .Where(z => z.IsActive == true)
                .Include(item => item.ConditionRulesEntity).ToList();

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

        public Task<RuleEntity> GetRuleEntityByComparingObject(List<ConditionRuleEntity> conditionRuleEntities)
        {
            throw new NotImplementedException();
        }

        public async Task<RuleEntity> GetRuleEntityByIdAsync(Int64 id)
        {
            var entity = _dbContext.RuleEntities.Where(z => z.IsActive == true)
                        .Include(item => item.ConditionRulesEntity)

                        .Include(item => item.actionRuleEntities)
                                .ThenInclude(actionEntity => actionEntity.actionRulePropertiesEntities)
                                .ThenInclude(actionEntity => actionEntity.ActionPropertyEntity)
                                .ThenInclude(actionEntity => actionEntity.ActionEntity)

                        .SingleOrDefault(z => z.Id == id)!;
            entity.Conditions = entity.ConditionRulesEntity;
            return entity;
        }

        public async Task<RuleEntity> GetRuleEntityByQueryExpression(string queryExpression)
        {
            var entity = _dbContext.RuleEntities
                    .Include(z => z.actionRuleEntities)
                    .Include(z => z.ConditionRulesEntity)
                .SingleOrDefault(item => item!.QueryExpression == queryExpression);
            return entity;
        }

        public async Task<IQueryable<RuleEntity>> Where(Func<RuleEntity, bool> predicate)
        {
            var entities = _dbContext.RuleEntities.Where(predicate).AsQueryable();
            return entities;
        }
    }
}


