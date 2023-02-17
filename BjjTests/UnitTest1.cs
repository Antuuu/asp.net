using System;
using System.Linq;
using Bjj.Controllers;
using Bjj.Data;
using Bjj.Models;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace BjjTests;


public class TestDatabaseFixture
{
    private const string ConnectionString = @"Data Source=bjj_test.db";
    private static readonly object _lock = new();
    private static bool _databaseInitialized;
    public TestDatabaseFixture()
    {
        lock (_lock)
        {
            if (!_databaseInitialized)
            {
                using (var context = CreateContext())
                {
                    context.Database.EnsureDeleted();
                    context.Database.EnsureCreated();

                    context.AddRange(
                        new Academy() {Name = "Academy1", Address = "Address1", HeadCoach = "HeadCoach1"},
                        new Academy() {Name = "Academy2", Address = "Address2", HeadCoach = "HeadCoach2"},
                        new Academy() {Name = "Academy3", Address = "Address3", HeadCoach = "HeadCoach3"},
                        new Fighter() {FirstName = "Fighter_Name1", LastName = "Fighter_Lastname1", DateOfBirth = DateTime.Now, WeightCategory = WeightClasses.Superheavy, BeltColour = BeltColours.Black, FAcademyId = 1},
                        new Fighter() {FirstName = "Fighter_Name2", LastName = "Fighter_Lastname2", DateOfBirth = DateTime.Now, WeightCategory = WeightClasses.Superheavy, BeltColour = BeltColours.Black, FAcademyId = 2},
                        new Fighter() {FirstName = "Winner_Name", LastName = "Winner_Lastname", DateOfBirth = DateTime.Now, WeightCategory = WeightClasses.Superheavy, BeltColour = BeltColours.Black, FAcademyId = 1},
                        new Fighter() {FirstName = "Looser_Name", LastName = "Looser_Lastname", DateOfBirth = DateTime.Now, WeightCategory = WeightClasses.Superheavy, BeltColour = BeltColours.Black, FAcademyId = 2},
                        new FightResultBy() {Name = "Kimura"},
                        new FightResultBy() {Name = "Balacha"},
                        new FightResultBy() {Name = "Taktarov"},
                        new Fight() {DateOfFight = DateTime.Now, Fighter1Id = 3, Fighter2Id = 4, WinnerId = 3, WeightCategory = WeightClasses.Superheavy, FightResultById = 3},
                        new Fight() {DateOfFight = DateTime.Now, Fighter1Id = 3, Fighter2Id = 4, WinnerId = 3, WeightCategory = WeightClasses.Superheavy, FightResultById = 3},
                        new Fight() {DateOfFight = DateTime.Now, Fighter1Id = 3, Fighter2Id = 4, WinnerId = 3, WeightCategory = WeightClasses.Superheavy, FightResultById = 3},
                        new Fight() {DateOfFight = DateTime.Now, Fighter1Id = 3, Fighter2Id = 4, WinnerId = 3, WeightCategory = WeightClasses.Superheavy, FightResultById = 3},
                        new Fight() {DateOfFight = DateTime.Now, Fighter1Id = 3, Fighter2Id = 4, WinnerId = 3, WeightCategory = WeightClasses.Superheavy, FightResultById = 3},
                        new Fight() {DateOfFight = DateTime.Now, Fighter1Id = 3, Fighter2Id = 4, WinnerId = 3, WeightCategory = WeightClasses.Superheavy, FightResultById = 3},
                        new Fight() {DateOfFight = DateTime.Now, Fighter1Id = 3, Fighter2Id = 4, WinnerId = 3, WeightCategory = WeightClasses.Superheavy, FightResultById = 3},
                        new Fight() {DateOfFight = DateTime.Now, Fighter1Id = 3, Fighter2Id = 4, WinnerId = 3, WeightCategory = WeightClasses.Superheavy, FightResultById = 3}
                    );
                    context.SaveChanges();
                }

                _databaseInitialized = true;
            }
        }
    }

    public AppDbContext CreateContext()
        => new AppDbContext(
            new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlite(ConnectionString)
                .Options);
}
public class BjjRepositoryTest : IClassFixture<TestDatabaseFixture>
{
    public BjjRepositoryTest(TestDatabaseFixture fixture)
        => Fixture = fixture;

    public TestDatabaseFixture Fixture { get; }

    
    [Fact]
    public void GetFighter_FullName()
    {
        using var context = Fixture.CreateContext();

        Assert.Equal("Fighter_Name1 Fighter_Lastname1", context.Fighters.FirstOrDefault(f => f.Id == 1).FullName);
    }
    
    [Fact]
    public void GetFighter_Academy()
    {
        using var context = Fixture.CreateContext();

        Assert.Equal("Academy1", context.Fighters.Include(a => a.FAcademy).FirstOrDefault(f => f.Id == 1).FAcademy.Name);
    }
    
    
    [Fact]
    public void GetAcademies_Count()
    {
        using var context = Fixture.CreateContext();
        Assert.Equal(3, context.Academies.ToList().Count);
    }
    
    [Fact]
    public void GetAcademie_Name()
    {
        using var context = Fixture.CreateContext();
        Assert.Equal("Academy1", context.Academies.FirstOrDefault(a => a.Id == 1).Name);
    }
    
    [Fact]
    public void GetFightWinner_FullName()
    {
        using var context = Fixture.CreateContext();
        Assert.Equal("Winner_Name Winner_Lastname", context.Fights.Include(f => f.Winner).FirstOrDefault(a => a.Id == 1).Winner.FullName);
    }
    
    [Fact]
    public void GetFightFinishBy_Name()
    {
        using var context = Fixture.CreateContext();
        Assert.Equal("Taktarov", context.Fights.Include(f => f.FightResultBy).FirstOrDefault(a => a.Id == 1).FightResultBy.Name);
    }
    
    [Fact]
    public void GetFight_Count()
    {
        using var context = Fixture.CreateContext();
        Assert.Equal(8, context.Fights.ToList().Count);
    }
    
    [Fact]
    public void GetFighter_Count()
    {
        using var context = Fixture.CreateContext();
        Assert.Equal(4, context.Fighters.ToList().Count);
    }
    
    [Fact]
    public void GetFightResultBy_Count()
    {
        using var context = Fixture.CreateContext();
        Assert.Equal(3, context.FightResultsBy.ToList().Count);
    }
    
    [Fact]
    public void GetAcademyHeadCoach_Name()
    {
        using var context = Fixture.CreateContext();
        Assert.Equal("HeadCoach1", context.Academies.FirstOrDefault(a => a.Id == 1).HeadCoach);
    }
        
}