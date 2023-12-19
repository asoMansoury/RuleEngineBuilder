using Microsoft.AspNetCore.Mvc;
using RuleBuilderInfra.Application.PresentationModels;
using RuleBuilderInfra.Application.Services.Contracts.RuleEngineer;
using RuleBuilderInfra.Application.Services.Contracts;
using RuleBuilderInfra.Domain.Entities;
using RuleBuilderInfra.Application.PresentationModels.RuleEngineModels;
using System.Reflection;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Newtonsoft.Json;
using RuleBuilderInfra.Application.PresentationModels.BuisinessEngineModels;
using RuleBuilderInfra.Application.Services;

namespace RuleBuilderInfra.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RuleEngineController : Controller
    {
        private readonly IConditionService _conditionService;
        private readonly IRuleBuilderEngineService<FakeDataEntity, FakeDataModel> _ruleBuilderEngineService;
        private readonly ICallingBusinessServiceMediator _callingBusinessServiceMediator;
        private readonly IRuleManagerService _ruleManagerService;
        public RuleEngineController(IConditionService conditionService,
                            IRuleBuilderEngineService<FakeDataEntity,
                            FakeDataModel> ruleBuilderEngineService,
                            ICallingBusinessServiceMediator callingBusinessServiceMediator,
                            IRuleManagerService ruleManagerService)
        {
            this._conditionService = conditionService;
            this._ruleBuilderEngineService = ruleBuilderEngineService;
            _callingBusinessServiceMediator = callingBusinessServiceMediator;
            _ruleManagerService = ruleManagerService;
        }

        [HttpGet(nameof(GetPropertyPairs))]
        public async Task<IActionResult> GetPropertyPairs(CancellationToken cancellationToken)
        {
            return Ok(await _ruleBuilderEngineService.GetPropertyPairs());
        }



        [HttpGet(nameof(GetTypeOfProperty))]
        public async Task<IActionResult> GetTypeOfProperty([FromQuery] string property)
        {
            try
            {
                return Ok(await _ruleBuilderEngineService.GetTypeOfPropertyName(property));
            }
            catch (ArgumentNullException ex)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


        [HttpPost(nameof(GenericDynamicQuery))]
        public async Task<ActionResult> GenericDynamicQuery([FromBody] RuleEntity ruleCondition)
        {
            var data = this._ruleBuilderEngineService.GenerateQueryBuilder(ruleCondition);
            return Ok(data);
        }

        [HttpPost(nameof(ExecuteMethod))]
        public async Task<IActionResult> ExecuteMethod([FromBody] RuleEntity ruleEntity)
        {
            _callingBusinessServiceMediator.InvokeAsync(ruleEntity.Id);
            return Ok(null);
        }

        [HttpGet(nameof(GetAllRules))]
        public async Task<IActionResult> GetAllRules()
        {
            return Ok(await _ruleManagerService.GetAll());
        }



    }
}
