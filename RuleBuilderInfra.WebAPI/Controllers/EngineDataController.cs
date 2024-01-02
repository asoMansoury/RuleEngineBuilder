using Microsoft.AspNetCore.Mvc;
using RuleBuilderInfra.Application.PresentationModels;
using RuleBuilderInfra.Application.Services;
using RuleBuilderInfra.Application.Services.Contracts;
using RuleBuilderInfra.Application.Services.Contracts.RuleEngineer;
using RuleBuilderInfra.Application.Services.Implementations.RuleEngineer;
using RuleBuilderInfra.Domain.Entities;
using RuleBuilderInfra.Persistence;
using System.Reflection;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RuleBuilderInfra.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EngineDataController : Controller
    {
        private readonly IFieldTypesService _fieldTypesService;
        private readonly IOperatorTypesService _operatorTypesService;
        private readonly IConditionService _conditionServices;
        private readonly ICallingBusinessServiceMediator<MainDatabase> _callingBusinessServiceMediator;
        public EngineDataController(IFieldTypesService fieldTypesService,
                                    IOperatorTypesService operatorTypesService,
                                    IConditionService conditionService,
                                    ICallingBusinessServiceMediator<MainDatabase> callingBusinessServiceMediator)
        {
            this._fieldTypesService = fieldTypesService;
            this._operatorTypesService = operatorTypesService;
            this._conditionServices = conditionService;
            this._callingBusinessServiceMediator = callingBusinessServiceMediator;
        }

        [HttpGet(nameof(GetAllFieldTypes))]
        public async Task<IActionResult> GetAllFieldTypes()
        {
            var types = await _fieldTypesService.GetFieldTypes();
            return Ok(types);
        }

        [HttpGet(nameof(GetAllCondition))]
        public async Task<IActionResult> GetAllCondition()
        {
            return Ok(await this._conditionServices.GetConditionEntitiesAsync());
        }

        [HttpGet(nameof(GetOperators))]
        public async Task<IActionResult> GetOperators([FromQuery]string fileType)
        {
            return Ok(await this._operatorTypesService.GetOperatorTypesAsync(fileType));
        }

    }
}
