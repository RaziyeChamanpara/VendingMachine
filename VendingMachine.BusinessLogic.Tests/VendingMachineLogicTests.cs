using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using VendingMachine.BusinessLogic;
using System.Collections.Generic;
using VendingMachine.Models;
using System.Linq;

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
      var can1 = new Can() { Id = 1, Count = 2, Price = 10 };
      var can2 = new Can() { Id = 2, Count = 2, Price = 20 };
      List<Can> cans = new List<Can>() { can1, can2 };

      VendingMachineLogic logic = new VendingMachineLogic(cans);

      //Act
      logic.Sell(can1, PaymentMethod.Credit);
      logic.Sell(can2, PaymentMethod.Cash);
      var results = logic.GetAvailableCash();

      //Assert
      results.Should().Be(20m);
    }

    [TestMethod]
    public void GetAvailableCredit_WhenSoldWithCash_ShouldIgnoreCash()
    {
      //Arrange
      var can1 = new Can() { Id = 1, Count = 2, Price = 10 };
      var can2 = new Can() { Id = 2, Count = 2, Price = 20 };
      List<Can> cans = new List<Can>() { can1, can2 };

      VendingMachineLogic logic = new VendingMachineLogic(cans);

      //Act
      logic.Sell(can1, PaymentMethod.Credit);
      logic.Sell(can2, PaymentMethod.Cash);
      var results = logic.GetAvailableCredit();

      //Assert
      results.Should().Be(10m);
    }

    [TestMethod]
    public void Sell_When0Count_ShouldFail()
    {
      //Arrange
      var can1 = new Can() { Id = 1, Count = 0, Price = 1.5m };
      var can2 = new Can() { Id = 2, Count = 2, Price = 5m };
      List<Can> cans = new List<Can>() { can1, can2 };

      VendingMachineLogic logic = new VendingMachineLogic(cans);

      //Act
      var result1 = logic.Sell(can1, PaymentMethod.Credit);
      var result2 = logic.Sell(can1, PaymentMethod.Cash);

      //Assert
      result1.Should().Be(false);
      result2.Should().Be(false);
    }

    [TestMethod]
    public void Sell_WhenPositiveCount_ShouldCollectMoney()
    {
      //Arrange
      var can1 = new Can() { Id = 1, Count = 1, Price = 1.5m };
      var can2 = new Can() { Id = 2, Count = 1, Price = 5m };
      List<Can> cans = new List<Can>() { can1, can2 };

      VendingMachineLogic logic = new VendingMachineLogic(cans);

      //Act
      var result1 = logic.Sell(can1, PaymentMethod.Cash);
      var result2 = logic.Sell(can2, PaymentMethod.Credit);
      var cash = logic.GetAvailableCash();
      var credit = logic.GetAvailableCredit();

      //Assert
      result1.Should().Be(true);
      result2.Should().Be(true);

      cash.Should().Be(1.5m);
      credit.Should().Be(5m);
    }

    [TestMethod]
    public void Restock_Always_ShouldResetCash()
    {
      //Arrange
      var can1 = new Can() { Id = 1, Count = 1, Price = 1.5m };
      var can2 = new Can() { Id = 2, Count = 1, Price = 5m };
      List<Can> cans = new List<Can>() { can1, can2 };

      VendingMachineLogic logic = new VendingMachineLogic(cans);

      //Act
      logic.Sell(can1, PaymentMethod.Cash);
      var cashBeforeRestock = logic.GetAvailableCash();
      logic.Restock(cans);
      var cashAfterRestock = logic.GetAvailableCash();

      //Assert
      cashBeforeRestock.Should().Be(1.5m);
      cashAfterRestock.Should().Be(0);

    }
    [TestMethod]
    public void Restock_Always_ShouldResetCredit()
    {
      //Arrange
      var can1 = new Can() { Id = 1, Count = 1, Price = 1.5m };
      var can2 = new Can() { Id = 2, Count = 1, Price = 5m };
      List<Can> cans = new List<Can>() { can1, can2 };

      VendingMachineLogic logic = new VendingMachineLogic(cans);

      //Act
      logic.Sell(can1, PaymentMethod.Credit);
      var creditBeforeRestock = logic.GetAvailableCredit();
      logic.Restock(cans);
      var creditAfterRestock = logic.GetAvailableCredit();

      //Assert
      creditBeforeRestock.Should().Be(1.5m);
      creditAfterRestock.Should().Be(0);
    }
    [TestMethod]
    public void Restock_Always_ShouldIncreseCounts()
    {
      //Arrange
      var can1 = new Can() { Id = 1, Count = 1, Price = 1.5m };
      var can2 = new Can() { Id = 2, Count = 1, Price = 5m };
      List<Can> cans = new List<Can>() { can1, can2 };

      VendingMachineLogic logic = new VendingMachineLogic(cans);

      //Act
      var cansBeforeRestock = logic.GetAvailableCans();
      logic.Restock(cans);
      var cansAfterRestock= logic.GetAvailableCans();

      //Assert
      cansBeforeRestock.First().Count.Should().Be(1);
      cansAfterRestock.First().Count.Should().Be(2);

    }
  }
}
