using Microsoft.EntityFrameworkCore;
using Bjj.Models;
using System;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace Bjj.Data
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new AppDbContext(
                       serviceProvider.GetRequiredService<DbContextOptions<AppDbContext>>()))
            {
                context.Database.EnsureCreated();

                if(context.Fighters.Any())
                {
                    return; // dane już zostały dodane do bazy danych
                }

                var fighter = new Fighter
                {
                    FirstName = "Kacper",
                    LastName = "Cesarz",
                    WeightCategory = WeightClasses.Superheavy,
                    BeltColour = BeltColours.Blue,
                    DateOfBirth = new DateTime(1998, 09, 20)
                };
                var fighter2 = new Fighter
                {
                    FirstName = "Stefan",
                    LastName = "Stefańki",
                    WeightCategory = WeightClasses.Light,
                    BeltColour = BeltColours.Black,
                    DateOfBirth = new DateTime(1998, 09, 20)
                };

                context.Fighters.Add(fighter);
                context.Fighters.Add(fighter2);

                context.SaveChanges();

                /* do uzupełnienia */
            }
        } 
    
    }
}