using Microsoft.AspNetCore.Mvc;

namespace Klinked.Cqrs.AspNetCore.Controllers
{
    [Route("")]
    [Route("[controller]")]
    public class Home : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}