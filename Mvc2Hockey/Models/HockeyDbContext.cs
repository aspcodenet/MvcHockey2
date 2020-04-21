using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Mvc2Hockey.Models
{
    public class HockeyDbContext : DbContext
    {
        public HockeyDbContext(DbContextOptions<HockeyDbContext> options)
            : base(options)
        {
        }

        public DbSet<EmailSubscriber> EmailSubscribers { get; set; }

        public DbSet<Player> Players { get; set; }
        public DbSet<Team> Team { get; set; }
    }
}
