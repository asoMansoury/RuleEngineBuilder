using ApplicationTest.Services;
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

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RuleBuilderInfra.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : Controller
    {

        private readonly ICategoryManagerService _assemblyManagerService;
        private readonly IActionService _actionService;
        public CategoriesController(ICategoryManagerService assemblyManagerService, IActionService actionService)
        {
            _assemblyManagerService = assemblyManagerService;
            _actionService = actionService;
           
        }

        [HttpGet(nameof(GetCategories))]
        public async Task<IActionResult> GetCategories(CancellationToken cancellationToken)
        {

            return Ok(_assemblyManagerService.GetAllCategories());
        }

        [HttpGet(nameof(GetBusinessServices))]
        public async Task<IActionResult> GetBusinessServices(CancellationToken cancellationToken)
        {
            var result =await _actionService.GetActions();
            return Ok(result);
        }

        [HttpGet(nameof(GetServiceInputProperties))]
        public async Task<IActionResult> GetServiceInputProperties([FromQuery] string CategoryService, [FromQuery] string ServiceName, CancellationToken cancellationToken)
        {

            return Ok(_assemblyManagerService.GetServiceInputProperties(CategoryService, ServiceName, cancellationToken));
        }
    }
}
