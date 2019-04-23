using System.Threading.Tasks;
using Klinked.Cqrs.AspNetCore.Common;
using Klinked.Cqrs.AspNetCore.Leagues.Models;
using Klinked.Cqrs.Queries;
using Microsoft.EntityFrameworkCore;

namespace Klinked.Cqrs.AspNetCore.Leagues.Queries
{
    public class GetLeagueByIdQueryArgs
    {
        public int Id { get; }

        public GetLeagueByIdQueryArgs(int id)
        {
            Id = id;
        }
    }
    
    public class GetLeagueByIdQueryHandler : IQueryHandler<GetLeagueByIdQueryArgs, League>
    {
        private readonly FootballContext _context;

        public GetLeagueByIdQueryHandler(FootballContext context)
        {
            _context = context;
        }

        public async Task<League> ExecuteAsync(GetLeagueByIdQueryArgs args)
        {
            return await _context.Leagues.SingleOrDefaultAsync(l => l.Id == args.Id);
        }
    }
}