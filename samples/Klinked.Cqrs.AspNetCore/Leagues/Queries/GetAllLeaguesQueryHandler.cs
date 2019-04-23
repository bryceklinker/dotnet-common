using System.Threading.Tasks;
using Klinked.Cqrs.AspNetCore.Common;
using Klinked.Cqrs.AspNetCore.Leagues.Models;
using Klinked.Cqrs.Queries;
using Microsoft.EntityFrameworkCore;

namespace Klinked.Cqrs.AspNetCore.Leagues.Queries
{
    public class GetAllLeaguesQueryArgs
    {
    }

    public class GetAllLeaguesQueryHandler : IQueryHandler<GetAllLeaguesQueryArgs, League[]>
    {
        private readonly FootballContext _context;

        public GetAllLeaguesQueryHandler(FootballContext context)
        {
            _context = context;
        }

        public async Task<League[]> ExecuteAsync(GetAllLeaguesQueryArgs args)
        {
            return await _context.Leagues.ToArrayAsync();
        }
    }
}