using Microsoft.AspNetCore.Mvc;
using RuleBuilderInfra.Application.PresentationModels;
using RuleBuilderInfra.Application.Services;
using RuleBuilderInfra.Application.Services.Contracts;

namespace RuleBuilderInfra.WebAPI.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class FakeDataController : Controller
    {
        
        private readonly IFakeDataService _fakeDataService;
        public FakeDataController(IFakeDataService fakeDataService)
        {
            _fakeDataService = fakeDataService;
        }

        [HttpGet(nameof(GetMovies))]
        public async Task<IActionResult> GetMovies(CancellationToken cancellationToken)
        {
            return Ok(await _fakeDataService.GetDistencteMovieAsync());
        }

        [HttpGet(nameof(GetProvinces))]
        public async Task<IActionResult> GetProvinces(CancellationToken cancellationToken)
        {
            return Ok(await _fakeDataService.GetDistencteProvincesAsync());
        }

        [HttpPost(nameof(ExecuteMethodByName))]
        public async Task<IActionResult> ExecuteMethodByName([FromBody] FakeDataModelSample ruleEntity, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _fakeDataService.CallTaxServices(ruleEntity, ruleEntity.EarnedAmount, cancellationToken);
                //var result = await _callingBusinessServiceMediator.InvokeAsync(1, "");
                return Ok(result);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
