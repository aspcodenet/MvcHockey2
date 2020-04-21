using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Mvc2Hockey.Models
{
    public class DatabaseInitializer
    {
        public void Initialize(HockeyDbContext context)
        {
            context.Database.EnsureCreated();
            context.Database.Migrate();
            SeedData(context);
        }

        private void SeedData(HockeyDbContext context)
        {
            if (!context.Team.Any(r => r.Name == "Djurgården"))
                context.Team.Add(new Team {Name = "Djurgården" });
            if (!context.Team.Any(r => r.Name == "AIK"))
                context.Team.Add(new Team { Name = "AIK" });
            if (!context.Players.Any(r => r.Name == "Mats Sundin"))
                context.Players.Add(new Player {Name = "Mats Sundin", Age = 49, JerseyNumber = 13});
            //Users - user -> role
            context.SaveChanges();
        }
    }
}
