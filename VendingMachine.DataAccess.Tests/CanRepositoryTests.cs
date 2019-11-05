using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using VendingMachine.Models;


namespace VendingMachine.DataAccess.Tests
{
  [TestClass]
  public class CanRepositoryTests
  {
    [TestMethod]
    public void GetAll_Always_ShouldReturnAllCans()
    {
      //Arrange
      List<Can> cans = new List<Can>()
      {
        new Can { Name = "Coka Cola" ,Count = 0},
        new Can { Name = "water" ,Count = 1},
        new Can { Name = "orange juice",Count = 0 },
        new Can { Name = "apple juice" ,Count = 2},
        new Can { Name = "pepsi" ,Count = 0},
        new Can { Name = "lemonade",Count = 0 },
      };

      CanRepository canRepository = new CanRepository(cans);

      //Act
      var allCans = canRepository.GetAll();

      //Assert
      allCans.Count.Should().Be(cans.Count);
    }

    [TestMethod]
    public void Get_WithId_ShouldFindTheCan()
    {
      //Arrange
      List<Can> cans = new List<Can>()
      {
        new Can { Name = "Coka Cola" ,Count = 0, Id=1 },
        new Can { Name = "water" ,Count = 1, Id=2 },
      };
      CanRepository canRepository = new CanRepository(cans);

      //Act
      var can = canRepository.Get(1);

      //Assert
      can.Name.Should().Be("Coka Cola");

    }

    [TestMethod]
    public void Remove_IfMoreThan1Can_ShouldDecreaseCount()
    {
      //Arrange
      List<Can> cans = new List<Can>()
      {
        new Can { Name = "water", Count = 2, Id = 1 },
        new Can { Name = "orange juice", Count = 2, Id = 2 }
      };
      CanRepository canRepository = new CanRepository(cans);

      //Act
      canRepository.Remove(1);

      //Assert
      var deletedCan = canRepository.Get(1);
      deletedCan.Count.Should().Be(1);
    }

    [TestMethod]
    public void Update_Always_ShouldUpdateAllFields()
    {
      //Arrange
      List<Can> cans = new List<Can>()
      {
        new Can {Name="apple juice", Count=1, Id=1 },
        new Can {Name="orange juice", Count=2, Id=2 }
      }; 
      CanRepository canRepository = new CanRepository(cans);
      Can can = new Can() { Name ="water", Count = 3, Id=1};
      
      //Act
      canRepository.Update(can);

      //Assert
      var updatedCan=canRepository.Get(1);
      updatedCan.Name.Should().Be("water");
      updatedCan.Count.Should().Be(3);

    }

  }
}
