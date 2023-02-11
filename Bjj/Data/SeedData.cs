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
                
                if(context.Fights.Any())
                {
                    return; // dane już zostały dodane do bazy danych
                }

                var submission1 = new FightResultBy
                {
                    Name = "Points"
                };
                
                var submission2 = new FightResultBy
                {
                    Name = "Kimura"
                };
                
                var submission3 = new FightResultBy
                {
                    Name = "DQ"
                };
                
                var fighter = new Fighter
                {
                    FirstName = "Kacper",
                    LastName = "Cesarz",
                    WeightCategory = WeightClasses.Light,
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

                var fight = new Fight
                {
                    Fighter1 = fighter,
                    Fighter2 = fighter2, 
                    Winner = fighter,
                    DateOfFight = new DateTime(1950, 01, 01),
                    WeightCategory = WeightClasses.Light,
                    FightResultBy = submission1
                };

                var fight2 = new Fight
                {
                    Fighter1 = fighter,
                    Fighter2 = fighter2, 
                    Winner = fighter,
                    DateOfFight = new DateTime(1900, 01, 01),
                    WeightCategory = WeightClasses.Light,
                    FightResultBy = submission2
                };
                
                var fight3 = new Fight
                {
                    Fighter1 = fighter,
                    Fighter2 = fighter, 
                    Winner = fighter,
                    DateOfFight = new DateTime(1900, 01, 01),
                    WeightCategory = WeightClasses.Light,
                    FightResultBy = submission2
                };
                
                
                context.Fighters.Add(fighter);
                context.Fighters.Add(fighter2);
                context.Fights.Add(fight);
                context.Fights.Add(fight2);
                context.Fights.Add(fight3);
                context.FightResultsBy.Add(submission1);
                context.FightResultsBy.Add(submission2);
                context.FightResultsBy.Add(submission3);

                context.SaveChanges();

                /* do uzupełnienia */
            }
        } 
    
    }
}