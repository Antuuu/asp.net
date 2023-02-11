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
                    .HasOne(f => f.Fighter1)
                    .WithMany()
                    .HasForeignKey(f => f.Fighter1Id)
                    .OnDelete(DeleteBehavior.Cascade);
                modelBuilder.Entity<Fight>()
                    .HasOne(f => f.Fighter2)
                    .WithMany()
                    .HasForeignKey(f => f.Fighter2Id)
                    .OnDelete(DeleteBehavior.Cascade);
                modelBuilder.Entity<Fight>()
                    .HasOne(f => f.Winner)
                    .WithMany()
                    .HasForeignKey(f => f.WinnerId)
                    .OnDelete(DeleteBehavior.Cascade);
                modelBuilder.Entity<Fight>()
                    .HasOne(f => f.FightResultBy)
                    .WithMany()
                    .HasForeignKey(f => f.FightResultById)
                    .OnDelete(DeleteBehavior.Cascade);

            }
            public virtual DbSet<Models.Fighter> Fighters { get; set; } = default!;
            public virtual DbSet<Models.Fight> Fights { get; set; } = default!;
            public virtual DbSet<Models.FightResultBy> FightResultsBy { get; set; } = default!;


            protected override void OnConfiguring(DbContextOptionsBuilder options)
            {
                options.UseSqlite(@"Data Source=bjj.db");
                options.EnableSensitiveDataLogging();
            }
        }

        /* do uzupełnienia */
}