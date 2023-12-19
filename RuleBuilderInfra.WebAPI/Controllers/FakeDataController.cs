using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> GetMovies()
        {
            return Ok(await _fakeDataService.GetDistencteMovieAsync());
        }

        [HttpGet(nameof(GetProvinces))]
        public async Task<IActionResult> GetProvinces()
        {
            return Ok(await _fakeDataService.GetDistencteProvincesAsync());
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
