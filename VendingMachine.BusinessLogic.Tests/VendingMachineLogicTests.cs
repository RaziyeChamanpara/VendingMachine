using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using VendingMachine.BusinessLogic;
using System.Collections.Generic;
using VendingMachine.Models;

namespace VendingMachine.BusinessLogic.Tests
{
  [TestClass]
  public class VendingMachineLogicTests
  {
    [TestMethod]
    public void GetAllCans_Always_ShouldReturnAllCans()
    {
      //Arrange
      List<Can> cans = new List<Can>()
      {
        new Can() { Id = 1, Count = 0 },
        new Can() { Id = 2, Count = 1 }
      };

      VendingMachineLogic VendingMachineLogic = new VendingMachineLogic(cans);

      //Act
      var results = VendingMachineLogic.GetAllCans();

      //Assert
      results.Count.Should().Be(cans.Count);
    }
  }
}
