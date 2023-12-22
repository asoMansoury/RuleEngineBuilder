using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RuleBuilderInfra.Application.PresentationModels;
using RuleBuilderInfra.Application.PresentationModels.BuisinessEngineModels;
using RuleBuilderInfra.Application.Services;
using RuleBuilderInfra.Application.Services.Contracts;
using RuleBuilderInfra.Application.Services.Contracts.RuleEngineer;
using RuleBuilderInfra.Application.Services.Implementations.RuleEngineer;
using RuleBuilderInfra.Domain.Entities;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RuleBuilderInfra.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScannerController : Controller
    {
        private readonly IScanEntitiesEngineService _scanEntitiesEngineService;
        private readonly ICallingBusinessServiceMediator _callingBusinessServiceMediator;
        private readonly ICategoryManagerService _assemblyManagerService;
        private readonly IRuleManagerService _ruleManagerService;
        public ScannerController(IScanEntitiesEngineService scanEntitiesEngineService,
                                 ICallingBusinessServiceMediator callingBusinessServiceMediator,
                                 ICategoryManagerService assemblyManagerService,
                                 IRuleManagerService ruleManagerService)
        {
            this._scanEntitiesEngineService = scanEntitiesEngineService;
            _callingBusinessServiceMediator = callingBusinessServiceMediator;
            _assemblyManagerService = assemblyManagerService;
            _ruleManagerService = ruleManagerService;
        }

        #region Property Scanner
        [HttpGet(nameof(GetAllScannableEntities))]
        public async Task<IActionResult> GetAllScannableEntities([FromQuery] string entityCategoryCode)
        {
            return Ok(_scanEntitiesEngineService.GetAllScannableEntities(entityCategoryCode));
        }


        [HttpGet(nameof(GetPropertyPairs))]
        public async Task<IActionResult> GetPropertyPairs([FromQuery] string entityCategoryCode, [FromQuery] string entityCode, CancellationToken cancellationToken)
        {
            return Ok(await _scanEntitiesEngineService.GetPropertyPairs(entityCategoryCode, entityCode));
        }


        [HttpGet(nameof(GetTypeOfProperty))]
        public async Task<IActionResult> GetTypeOfProperty([FromQuery] string entityCategoryCode, [FromQuery] string entityCode, [FromQuery] string propertyName, CancellationToken cancellationToken)
        {
            return Ok(await _scanEntitiesEngineService.GetTypeOfPropertyName(entityCategoryCode, entityCode, propertyName));
        }
        #endregion


        #region Query Builder Services
        [HttpPost(nameof(GenericDynamicQuery))]
        public async Task<ActionResult> GenericDynamicQuery([FromBody] RuleEntity ruleCondition)
        {
            var data = await this._scanEntitiesEngineService.GenerateQueryBuilder(ruleCondition);
            return Ok((JsonConvert.SerializeObject(data)));
        }


        [HttpPost(nameof(ExecuteMethod))]
        public async Task<IActionResult> ExecuteMethod([FromBody] RuleEntity ruleEntity)
        {
            var data = await this._scanEntitiesEngineService.GenerateQueryBuilder(ruleEntity);
            _callingBusinessServiceMediator.InvokeAsync(ruleEntity.CategoryService, ruleEntity.ServiceName, JsonConvert.SerializeObject(data), ruleEntity.Value.ToString());
            return Ok(data);
        }


        [HttpPost(nameof(ExecuteMethodByRuleEntityID))]
        public async Task<IActionResult> ExecuteMethodByRuleEntityID([FromBody] RunningSavedRule ruleEntity)
        {
            var result = await _callingBusinessServiceMediator.InvokeAsync(ruleEntity.Id, ruleEntity.Value);
            return Ok(result);
        }
        #endregion


        #region Our Services


        [HttpGet(nameof(GetCategories))]
        public async Task<IActionResult> GetCategories(CancellationToken cancellationToken)
        {
            return Ok(_assemblyManagerService.GetAllCategories());
        }


        [HttpGet(nameof(GetBusinessServices))]
        public async Task<IActionResult> GetBusinessServices(CancellationToken cancellationToken)
        {
            return Ok(_assemblyManagerService.GetBusinessServices());
        }


        #endregion


        #region Rules Service

        [HttpGet(nameof(GetRules))]
        public async Task<IActionResult> GetRules()
        {
            var rules = await _ruleManagerService.GetAllRulesAsync();
            var data = await this._scanEntitiesEngineService.GenerateQueryBuilder(rules.LastOrDefault());
            return Ok(rules);
        }


        [HttpPost(nameof(SaveRule))]
        public async Task<IActionResult> SaveRule([FromBody] RuleEntity ruleEntity)
        {
            await _ruleManagerService.AddNewRuleAsync(ruleEntity);
            return Ok();
        }
        #endregion

    }
}
