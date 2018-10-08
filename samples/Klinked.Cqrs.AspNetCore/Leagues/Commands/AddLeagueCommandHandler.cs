using System.Threading.Tasks;
using Klinked.Cqrs.AspNetCore.Common;
using Klinked.Cqrs.AspNetCore.Leagues.Models;
using Klinked.Cqrs.Commands;

namespace Klinked.Cqrs.AspNetCore.Leagues.Commands
{
    public class AddLeagueCommandArgs
    {
        public int Id { get; set; }
        public string Name { get; }

        public AddLeagueCommandArgs(string name)
        {
            Name = name;
        }
    }
    
    public class AddLeagueCommandHandler : ICommandHandler<AddLeagueCommandArgs>
    {
        private readonly FootballContext _context;

        public AddLeagueCommandHandler(FootballContext context)
        {
            _context = context;
        }

        public async Task Execute(AddLeagueCommandArgs args)
        {
            var entry = _context.Add(new League {Name = args.Name});
            await _context.SaveChangesAsync();
            args.Id = entry.Entity.Id;
        }
    }
}