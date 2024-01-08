using RuleBuilderInfra.Application.PresentationModels;
using RuleBuilderInfra.Application.PresentationModels.BuisinessEngineModels;
using RuleBuilderInfra.Application.Services.Contracts;
using RuleBuilderInfra.Domain.Entities;
using RuleBuilderInfra.Persistence;
using RuleBuilderInfra.Persistence.Repositories.Contracts;
using System.Collections.Generic;
using System.Text;

namespace RuleBuilderInfra.Application.Services.Implementations
{
    public class FakeDataService : BaseService, IFakeDataService
    {
        private readonly IFakeDataRepository _fakeDataRepository;

        private readonly IConditionRepository _conditionRepository;
        private readonly IRuleEntityRepository _ruleEntityRepository;
        private readonly IRuleManagerService _ruleManagerService;
        private readonly ICallingBusinessServiceMediator<MainDatabase> _callingBusinessServiceMediator;
        public FakeDataService(IUnitOfWork unitOfWork,
                                IFakeDataRepository fakeDataRepository,
                                IRuleEntityRepository ruleEntityRepository,
                                IConditionRepository conditionRepository,
                                ICallingBusinessServiceMediator<MainDatabase> callingBusinessServiceMediator,
                                IRuleManagerService ruleManagerService) : base(unitOfWork)
        {
            this._fakeDataRepository = fakeDataRepository;
            this._ruleEntityRepository = ruleEntityRepository;
            this._conditionRepository = conditionRepository;
            _callingBusinessServiceMediator = callingBusinessServiceMediator;
            _ruleManagerService = ruleManagerService;
        }

        public async Task<List<FakeDataEntity>> GetDistencteMovieAsync()
        {
            return await _fakeDataRepository.GetDistinctByMovies();
        }

        public async Task<List<FakeDataEntity>> GetDistencteProvincesAsync()
        {

            return await _fakeDataRepository.GetDistinctByProvinces();
        }

        public async Task<List<FakeDataEntity>> GetDistributerByMovieOrProvinceAsync(string province, string movie)
        {
            try
            {
                return await _fakeDataRepository.GetDistributerByMovieOrProvinceAsync(province, movie);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        public async Task<TaxCalculatorForFakeModelResponse> CallTaxServices(FakeDataModelSample sampleData, int EarnedAmount, CancellationToken cancellationToken)
        {
            try
            {
                var generatedQuery =await _ruleManagerService.GetOrderedExpressiont(CommonUtility.GeneratingQuery(sampleData.RuleEntity));
                var ruleEntity = await _ruleEntityRepository.GetRuleEntityByQueryExpression(generatedQuery);
                if (ruleEntity == null)
                    throw new Exception("Not found any rule for provided data");
                var convertedObject = ConvertObjectToConditionRuleEntity(sampleData.RuleEntity);
                var foundCondition = _ruleManagerService.FindConditionRuleEntity(convertedObject);

                var clientParams = new TaxCalculatorClient { EarnedAmount = sampleData.EarnedAmount };
                var result = _callingBusinessServiceMediator.InvokeAsync(ruleEntity.Id, clientParams).Result as TaxCalculatorForFakeModelResponse;
                return result;

            }
            catch (Exception ex)
            {

                throw ex;
            }
            throw new NotImplementedException();
        }


        private List<ConditionRuleEntity> ConvertObjectToConditionRuleEntity(FakeRule fakeRule)
        {
            var result = new List<ConditionRuleEntity>();
            var properties = fakeRule.GetType().GetProperties();
            var totalRecords = properties.Length - 1;
            int currentIndex = 0;
            var conditionEntity = _conditionRepository.GetConditionsByCodes("AND").Result.FirstOrDefault();

            ConditionRuleEntity element = null;

            foreach (var item in properties)
            {
                element = new ConditionRuleEntity();
                element.ConditionEntity = conditionEntity;
                var PropertyName = item.Name;
                var PropertyValue = item.GetValue(fakeRule).ToString();
                var OperatorCode = "Eq";

                element.Id = currentIndex;
                element.ParentId = result.Count == 0 ? null : result.FirstOrDefault().Id;
                element.Parent = result.Count == 0 ? null : result.FirstOrDefault();
                element.PropertyName = PropertyName;
                element.Operator = OperatorCode;
                element.Value = PropertyValue;
                result.Add(element);
                currentIndex++;
            }
            return result;
        }





    }
}
