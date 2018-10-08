using Klinked.Cqrs.AspNetCore.Leagues.Models;
using Microsoft.EntityFrameworkCore;

namespace Klinked.Cqrs.AspNetCore.Common
{
    public class FootballContext : DbContext
    {
        public DbSet<League> Leagues { get; private set; }
        
        public FootballContext(DbContextOptions options)
            : base(options)
        {
            
        }
    }
}