using Microsoft.EntityFrameworkCore;
using RuleBuilderInfra.Domain.Entities;
using RuleBuilderInfra.Persistence.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuleBuilderInfra.Persistence.Repositories.Implementations.RuleEntityService
{
    public class RefineEntitiesService
    {
        private readonly IRuleEntityRepository _ruleEntityRepository;
        private readonly IConditionRuleEntityRepository _conditionRuleEntityRepository;
        public RefineEntitiesService(IRuleEntityRepository ruleEntityRepository,
                                    IConditionRuleEntityRepository conditionRuleEntityRepository)
        {
            this._ruleEntityRepository = ruleEntityRepository;
            this._conditionRuleEntityRepository = conditionRuleEntityRepository;
        }
        public List<ConditionRuleEntity> RefineElements(List<ConditionRuleEntity> conditionRuleEntities)
        {
            Queue<ConditionRuleEntity> queueCondtion = new Queue<ConditionRuleEntity>();
            conditionRuleEntities.ForEach(conditionRuleEntity => { queueCondtion.Enqueue(conditionRuleEntity); });

            var result = new List<ConditionRuleEntity>();
            result.Add(queueCondtion.Dequeue());
            foreach (var queuItem in queueCondtion)
            {
                if (!result.Any(t => t.RuleEntityId == queuItem.RuleEntityId))
                {
                    result.Add(queuItem);
                }
            }
            //First Be Sure to just Get Records with different Rule Item

            var step2 = LoadRefinedElementsDependencies(result);
            return result;
        }

        private List<ConditionRuleEntity> LoadRefinedElementsDependencies(List<ConditionRuleEntity> conditionRuleEntities)
        {
            var result = new List<ConditionRuleEntity>();
            result.AddRange(conditionRuleEntities.Where(e => e.ParentId == null).ToList());

            var notRootelementIds = conditionRuleEntities.Where(e => e.ParentId != null).Select(z => z.ParentId).ToList();
            var foundNonRooElements = _conditionRuleEntityRepository.Where(t => notRootelementIds.Any(notRootItem => notRootItem == t.Id)).Result
                .Include(condition => condition.Conditions)
                .Include(ruleItem => ruleItem.RuleEntity)
                .Include(condition => condition.ConditionEntity)
                .ToList();
            result.AddRange(foundNonRooElements);
            return result;
        }
    }
}
