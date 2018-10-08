using System.Threading.Tasks;
using Klinked.Cqrs.AspNetCore.Leagues.Commands;
using Klinked.Cqrs.AspNetCore.Leagues.Models;
using Klinked.Cqrs.AspNetCore.Leagues.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Klinked.Cqrs.AspNetCore.Controllers
{
    [Route("[controller]")]
    public class LeaguesController : Controller
    {
        private readonly ICqrsBus _bus;

        public LeaguesController(ICqrsBus bus)
        {
            _bus = bus;
        }

        [HttpGet]
        public async Task<ActionResult> List()
        {
            var leagues = await _bus.Execute<GetAllLeaguesQueryArgs, League[]>(new GetAllLeaguesQueryArgs());
            return View("List", leagues);
        }

        [HttpGet("detail/{id:int}")]
        public async Task<ActionResult> Detail(int id)
        {
            var league = await _bus.Execute<GetLeagueByIdQueryArgs, League>(new GetLeagueByIdQueryArgs(id));
            return View("Detail", league);
        }

        [HttpGet("add")]
        public ActionResult Add()
        {
            return View("Add");
        }

        [HttpPost("Add")]
        public async Task<ActionResult> Add([FromForm] League league)
        {
            var args = new AddLeagueCommandArgs(league.Name);
            await _bus.Execute(args);
            return RedirectToAction("Detail", new {id = args.Id});
        }
    }
}