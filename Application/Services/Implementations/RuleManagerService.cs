
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using RuleBuilderInfra.Application.Services.Contracts;
using RuleBuilderInfra.Domain.Entities;
using RuleBuilderInfra.Persistence;
using RuleBuilderInfra.Persistence.Repositories.Contracts;
using RuleBuilderInfra.Persistence.Repositories.Implementations;
using RuleBuilderInfra.Persistence.Repositories.Implementations.RuleEntityService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace RuleBuilderInfra.Application.Services.Implementations
{
    public class RuleManagerService : BaseService, IRuleManagerService
    {
        private readonly IRuleEntityRepository _ruleEntityRepository;
        private readonly IConditionRuleEntityRepository _conditionRuleEntityRepository;
        private readonly IActionEntityRepository _actionEntityRepository;
        private readonly ICategoryManagerService _categoryManagerService;
        private readonly IActionRuleEntityRepository _actionRuleEntityRepository;
        private readonly IActionRulePropertiesEntityRepository _actionRulePropertiesEntityRepository;
        private readonly IConditionRepository _conditionRepository;
        public RuleManagerService(IUnitOfWork unitOfWork,
                                  IRuleEntityRepository ruleEntityRepository,
                                  IConditionRuleEntityRepository conditionRuleEntityRepository,
                                  ICategoryManagerService categoryManagerService,
                                  IActionEntityRepository actionEntityRepository,
                                  IActionRulePropertiesEntityRepository actionRulePropertiesEntityRepository,
                                  IConditionRepository conditionRepository,
                                  IActionRuleEntityRepository actionRuleEntityRepository) : base(unitOfWork)
        {
            this._ruleEntityRepository = ruleEntityRepository;
            this._conditionRuleEntityRepository = conditionRuleEntityRepository;
            this._categoryManagerService = categoryManagerService;
            this._actionEntityRepository = actionEntityRepository;
            this._actionRuleEntityRepository = actionRuleEntityRepository;
            this._actionRulePropertiesEntityRepository = actionRulePropertiesEntityRepository;
            this._conditionRepository = conditionRepository;
        }

        public async Task AddNewRuleAsync(RuleEntity ruleEntity)
        {
            try
            {
                var actionEntity = await _actionEntityRepository.GetActionEntityByName(ruleEntity.ServiceName);
                var actionRuleEntity = await this._actionRuleEntityRepository.AddActionRuleEntity(ruleEntity, actionEntity);

                Type inputBusinessModel = _categoryManagerService.GetInuptBusiness(actionEntity.CategoryService, actionEntity.ServiceName);
                var inputModel = JsonConvert.DeserializeObject(ruleEntity.JsonValue, inputBusinessModel);
                foreach (var actionProperty in actionEntity.ActionPropertis)
                {
                    var value = inputModel.GetType().GetProperty(actionProperty.PropertyName);
                    _actionRulePropertiesEntityRepository.AddActionRuleEntity(actionRuleEntity, actionProperty, value.GetValue(inputModel).ToString());
                }

                var condtions = await _conditionRepository.GetAllConditions();
                var entity = await _ruleEntityRepository.AddAsync(ruleEntity);
                ruleEntity.ConditionRulesEntity = entity.Conditions;
                SetCondtionEntityToRuleCondtions(entity.Conditions, condtions);

                var generateUniqueQuery = GetOrderedExpressiont(GeneratingStringExpression(entity.Conditions)).Result;
                if (_ruleEntityRepository.Where(z => z.QueryExpression == generateUniqueQuery && z.IsActive == true).Result.Count() > 0)
                {
                    throw new Exception("Duplicated condition");
                }
                entity.QueryExpression = generateUniqueQuery;
                _unitOfWork.CommitToDatabase();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private string GeneratingStringExpression(List<ConditionRuleEntity> conditionRuleEntities)
        {
            StringBuilder sb = new StringBuilder();
            var totalRecords = conditionRuleEntities.Count - 1;

            int currentIndex = 0;
            foreach (var conditionRuleEntity in conditionRuleEntities)
            {
                if (conditionRuleEntity.Conditions == null)
                    conditionRuleEntity.Conditions = new List<ConditionRuleEntity>();
                //var conditionCode = (currentIndex == totalRecords) ? ""
                //                  : conditionRuleEntity.ConditionCode != null ? conditionRuleEntity.ConditionCode
                //                                                                 : "AND ";

                var conditionCode = "";
                if (conditionRuleEntity.ConditionCode != null)
                {
                    conditionCode = conditionRuleEntity.ConditionCode;
                }
                else
                {
                    conditionCode = (conditionRuleEntity.Conditions.Count > 0) ? "AND " : ((conditionRuleEntity.Conditions.Count == 0 && currentIndex != totalRecords) ? "AND " : "");

                }

                string generatedQuery = $"{conditionRuleEntity.PropertyName} {conditionRuleEntity.Operator} {conditionRuleEntity.Value} {conditionCode}";
                currentIndex++;

                sb.Append(generatedQuery);
                if (conditionRuleEntity.Conditions != null && conditionRuleEntity.Conditions.Count > 0)
                    sb.Append(GeneratingStringExpression(conditionRuleEntity.Conditions));
            }
            return sb.ToString().Trim();
        }

        private void SetCondtionEntityToRuleCondtions(List<ConditionRuleEntity> conditionRuleEntities, List<ConditionEntity> conditionEntities)
        {
            var selectedCondtion = conditionEntities.FirstOrDefault();
            foreach (var conditionItem in conditionRuleEntities)
            {
                conditionItem.ConditionEntity = selectedCondtion;
                if (conditionItem.Conditions != null && conditionItem.Conditions.Count > 0)
                    SetCondtionEntityToRuleCondtions(conditionItem.Conditions, conditionEntities);
            }

        }




        public async Task<List<RuleEntity>> GetAll()
        {
            var ruleEntities = await _ruleEntityRepository.GetAllAsync();
            return ruleEntities;
        }

        public async Task<IEnumerable<RuleEntity>> GetAllRulesAsync()
        {
            return await _ruleEntityRepository.GetAllAsync();
        }

        public async Task<RuleEntity> GetRuleEntityByIdAsync(Int64 id)
        {
            return await _ruleEntityRepository.GetRuleEntityByIdAsync(id);
        }

        public async Task<RuleEntity> FindConditionRuleEntity(List<ConditionRuleEntity> entities)
        {
            ConditionRuleEntity resultEntity = new ConditionRuleEntity();
            int totalRecords = entities.Count;
            int foundedRecrods = 0;

            foreach (var entity in entities)
            {
                var propertyName = entity.PropertyName;
                var operatorCode = entity.Operator;
                var propertyValue = entity.Value;
                var conditionEntity = entity.ConditionEntity;

                var foundedConditionRules = _conditionRuleEntityRepository.Where(z => z.PropertyName == propertyName &&
                                                                                            z.Value == propertyValue &&
                                                                                            z.Operator == operatorCode &&
                                                                                            z.ConditionEntity == conditionEntity).Result.ToList();

                if (foundedConditionRules.Count > 0)
                {
                    var refinedElementsService = new RefineEntitiesService(_ruleEntityRepository, _conditionRuleEntityRepository);
                    var remainConditionAfterRefining = refinedElementsService.RefineElements(foundedConditionRules);

                    resultEntity = foundedConditionRules.Where(e => e.RuleEntityId != null).FirstOrDefault() != null ? foundedConditionRules.Where(e => e.RuleEntityId != null).Single()
                                                                                                                       : resultEntity;
                    foundedRecrods++;
                }
                else
                {
                    break;
                }
            }

            if (totalRecords == foundedRecrods)
            {
                try
                {
                    var result = _ruleEntityRepository.Where(z => z.Id == resultEntity.RuleEntityId).Result
                                    .Include(item => item.ConditionRulesEntity)
                                    .Include(z => z.actionRuleEntities)
                                        .ThenInclude(z => z.ActionEntity)
                                        .ThenInclude(z => z.ActionPropertis)
                                        .ThenInclude(z => z.actionRulePropertiesEntities)

                                    .SingleOrDefault();

                    return result;
                }
                catch (Exception ex)
                {

                    throw ex;
                }

            }
            throw new ArgumentException();
        }

        public async Task<string> GetOrderedExpressiont(string queryExpression)
        {
            StringBuilder queryBuilder = new StringBuilder();
            queryExpression.ToCharArray().Order().ToList().ForEach(t =>
            {
                queryExpression = queryExpression + t;
                queryBuilder.Append(t);
            });
            return queryBuilder.ToString().Trim();
        }
    }



}
