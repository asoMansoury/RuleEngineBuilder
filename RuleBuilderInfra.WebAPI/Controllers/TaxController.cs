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
    public class TaxController : Controller
    {

        private readonly ITaxCalculatorService _taxCalculatorService;
        public TaxController(ITaxCalculatorService taxCalculatorService)
        {
            this._taxCalculatorService = taxCalculatorService;
           
        }

        [HttpPost(nameof(TaxProvince))]
        public async Task<IActionResult> TaxProvince(TaxlCalcModelRequest request,CancellationToken cancellationToken)
        {
            return Ok(await _taxCalculatorService.TaxlCalcModelResponse(request));
        }
    }
}
