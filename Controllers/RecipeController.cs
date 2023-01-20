using Microsoft.Extensions.Logging;

namespace AllSpice.Controllers
{
    [Route("[controller]")]
    public class RecipeController : Controller
    {
        private readonly ILogger<Recipe> _logger;

        public RecipeController(ILogger<Recipe> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}