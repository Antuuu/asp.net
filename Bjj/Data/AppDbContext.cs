using Bjj.Models;
using Microsoft.EntityFrameworkCore;

namespace Bjj.Data
{
    public class AppDbContext : DbContext
        {
            public AppDbContext(DbContextOptions<AppDbContext> options)
                : base(options)
            {
            }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                base.OnModelCreating(modelBuilder);
                modelBuilder.Entity<Fight>()
                    .HasOne(x => x.Fighter1)
                    .WithMany(x => x.Fights1)
                    .HasForeignKey(x => x.Fighte1Id)
                    .OnDelete(DeleteBehavior.ClientSetNull);
                
                modelBuilder.Entity<Fight>()
                    .HasOne(x => x.Fighter2)
                    .WithMany(x => x.Fights2)
                    .HasForeignKey(x => x.Fighte2Id)
                    .OnDelete(DeleteBehavior.ClientSetNull);
                
                modelBuilder.Entity<Fight>()
                    .HasOne(x => x.Winner)
                    .WithMany(x => x.Winner)
                    .HasForeignKey(x => x.WinnerId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
                
                modelBuilder.Entity<Fight>()
                    .HasOne(x => x.FightResultBy)
                    .WithMany(x => x.Fights)
                    .HasForeignKey(x => x.FightResultById)
                    .OnDelete(DeleteBehavior.ClientSetNull);

            }
            public virtual DbSet<Models.Fighter> Fighters { get; set; } = default!;
            public virtual DbSet<Models.Fight> Fights { get; set; } = default!;
            public virtual DbSet<Models.FightResultBy> FightResultsBy { get; set; } = default!;


            protected override void OnConfiguring(DbContextOptionsBuilder options)
                => options.UseSqlite(@"Data Source=bjj.db");
        }

        /* do uzupełnienia */
}