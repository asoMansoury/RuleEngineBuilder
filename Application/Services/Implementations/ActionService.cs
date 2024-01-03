using RuleBuilderInfra.Application.Mapping;
using RuleBuilderInfra.Application.PresentationModels;
using RuleBuilderInfra.Application.Services.Contracts;
using RuleBuilderInfra.Domain.Entities;
using RuleBuilderInfra.Persistence;
using RuleBuilderInfra.Persistence.Repositories.Contracts;

namespace RuleBuilderInfra.Application.Services.Implementations
{
    public class ActionService : BaseService, IActionService
    {
        private readonly IActionEntityRepository _actionRepository;
        private readonly IActionPropertiesEntityRepository _actionPropertiesEntityRepository;
        private readonly ICategoryManagerService _categoryManagerService;

        public ActionService(IUnitOfWork unitOfWork, IActionEntityRepository actionRepository,
                                                      IActionPropertiesEntityRepository actionPropertiesEntityRepository,
                                                      ICategoryManagerService categoryManagerService) : base(unitOfWork)
        {
            _actionRepository = actionRepository;
            _actionPropertiesEntityRepository = actionPropertiesEntityRepository;
            _categoryManagerService = categoryManagerService;
        }

        public async Task<List<BusinessServiceModel>> GetActions()
        {
            var result = new List<BusinessServiceModel>();
            var actionEntities = await _actionRepository.GetAllAsync();
            result = ManuallMapping.Map(actionEntities);
            return result;
        }

        public async Task UpdateActions()
        {
            var actionEntities = _actionRepository.Where(e => e.IsActive==true).Result;
            var runtimeActionEntities = _categoryManagerService.GetBusinessServices();
            var newActionsNotInsertedInDatabase = runtimeActionEntities.Where(r =>
                                                                            !actionEntities.Any(e => e.ServiceName == r.ServiceName) &&
                                                                            !actionEntities.Any(e => e.CategoryService == r.CategoryService) &&
                                                                            !actionEntities.Any(e => e.ServiceAssembly == r.ServiceAssembly)).ToList();
            foreach (var newBusinessItem in newActionsNotInsertedInDatabase)
            {
                AddNewAction(newBusinessItem);
            }
           await base.SaveChangesAsync();
        }

        private void AddNewAction(BusinessServiceModel business)
        {
            var actionEntity = new ActionEntity();
            actionEntity.ServiceAssembly = business.ServiceAssembly;
            actionEntity.ServiceName = business.ServiceName;
            actionEntity.CategoryService = business.CategoryService;
            actionEntity.IsActive = true;
            var actionEntityAdded = _actionRepository.AddAsync(actionEntity);

            foreach (var param in business.InputParams)
            {
                var ActionPropertiesEntity = new ActionPropertiesEntity()
                {
                    ActionEntity = actionEntity,
                    PropertyName = param.PropertyName,
                    PropertyType = param.PropertyType,
                    IsActive = true
                };
                _actionPropertiesEntityRepository.AddAsync(ActionPropertiesEntity, actionEntity);
            }
        }


        public async Task InsertScannedActions()
        {
            UpdateActions();
            if (await _actionRepository.HasAnyAsync() == false)
            {
                var getAllBusinessServies = _categoryManagerService.GetBusinessServices();
                foreach (var business in getAllBusinessServies)
                {
                    AddNewAction(business);
                }
            }
            base.SaveChanges();
        }
    }
}
