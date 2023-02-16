using System;
using Bjj.Models;
using Xunit;

namespace BjjTests;

public class BjjRepositoryTest
{
   
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
        
}