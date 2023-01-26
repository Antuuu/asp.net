using Microsoft.EntityFrameworkCore;

namespace Bjj.Data
{
    public class AppDbContext : DbContext
        {
            public AppDbContext(DbContextOptions<AppDbContext> options)
                : base(options)
            {
            }
            public DbSet<Models.Fighter> Fighters { get; set; } = default!;
            public DbSet<Models.Fight> Fights { get; set; } = default!;
            public DbSet<Models.FightResultBy> FightResultsBy { get; set; } = default!;


            protected override void OnConfiguring(DbContextOptionsBuilder options)
                => options.UseSqlite(@"Data Source=bjj.db");
        }

        /* do uzupełnienia */
}