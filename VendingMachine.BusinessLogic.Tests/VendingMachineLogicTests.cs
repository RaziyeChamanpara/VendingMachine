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

    [TestMethod]
    public void GetAvailableCans_With0Counts_ShouldReturnAvailables()
    {
      //Arrange
      List<Can> cans = new List<Can>()
      {
        new Can() { Id = 1, Count = 0 },
        new Can() { Id = 2, Count = 0 },
        new Can() { Id = 3, Count = 0 },
        new Can() { Id = 4, Count = 1 }
      };

      VendingMachineLogic VendingMachineLogic = new VendingMachineLogic(cans);

      //Act
      var results = VendingMachineLogic.GetAvailableCans();

      //Assert
      results.Count.Should().Be(1);
    }

    [TestMethod]
    public void GetAvailableCash_WhenSoldWithCredit_ShouldIgnoreCredits()
    {
      //Arrange
      var can1 = new Can() { Id = 4, Count = 2, Price = 10 };
      var can2 = new Can() { Id = 4, Count = 2, Price = 20 };
      List<Can> cans = new List<Can>() { can1, can2 };

      VendingMachineLogic logic = new VendingMachineLogic(cans);

      //Act
      logic.Sell(can1, PaymentMethod.Credit);
      logic.Sell(can2, PaymentMethod.Cash);
      var results = logic.GetAvailableCash();

      //Assert
      results.Should().Be(20);
    }

    [TestMethod]
    public void GetAvailableCredit_WhenSoldWithCash_ShouldIgnoreCash()
    {
      //Arrange
      var can1 = new Can() { Id = 4, Count = 2, Price = 10 };
      var can2 = new Can() { Id = 4, Count = 2, Price = 20 };
      List<Can> cans = new List<Can>() { can1, can2 };

      VendingMachineLogic logic = new VendingMachineLogic(cans);

      //Act
      logic.Sell(can1, PaymentMethod.Credit);
      logic.Sell(can2, PaymentMethod.Cash);
      var results = logic.GetAvailableCredit();

      //Assert
      results.Should().Be(10);
    }
  }
}
