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
                        new Academy() {Name = "Academy3", Address = "Address3", HeadCoach = "HeadCoach3"});
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
        var academy = new Academy
        {
            Name = "Test_Academy",
            HeadCoach = "Test_Headcoach",
            Address = "Test_Address"
        };
        var fighter = new Fighter
        {
            FirstName = "Test_FirstName",
            LastName = "Test_LastName",
            DateOfBirth = new DateTime(1998, 09, 20),
            WeightCategory = (WeightClasses) 1,
            BeltColour = (BeltColours) 1,
            FAcademy = academy,
        };
        Assert.Equal("Test_FirstName Test_LastName", fighter.FullName);
    }
    
    [Fact]
    public void GetAcademies_Count()
    {
        using var context = Fixture.CreateContext();
        var controller = new AcademyController(context);

        Assert.Equal(3, context.Academies.ToList().Count);
    }
    
    [Fact]
    public void GetAcademie_Name()
    {
        using var context = Fixture.CreateContext();
        var controller = new AcademyController(context);

        Assert.Equal("Academy1", context.Academies.FirstOrDefault(a => a.Id == 1).Name);
    }
        
}