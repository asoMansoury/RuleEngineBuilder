
using Newtonsoft.Json;
using RuleBuilderInfra.Application.Services.Contracts;
using RuleBuilderInfra.Domain.Entities;
using RuleBuilderInfra.Persistence;
using RuleBuilderInfra.Persistence.Repositories.Contracts;
using RuleBuilderInfra.Persistence.Repositories.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                _unitOfWork.CommitToDatabase();
            }
            catch (Exception ex)
            {

                throw ex;
            }
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

        public async Task<RuleEntity> GetRuleEntityByIdAsync(int id)
        {
            return await _ruleEntityRepository.GetRuleEntityByIdAsync(id);
        }
    }



}
