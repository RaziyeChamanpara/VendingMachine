using Microsoft.VisualStudio.TestTools.UnitTesting;
using VendingMachine.Models;
using FluentAssertions;

namespace VendingMachine.DataAccess.Tests
{
  [TestClass]
  public class MoneyRepositoryTests
  {
    [TestMethod]
    public void ResetCredit_Always_ShouldSetCreditToZero()
    {
      //Arrange
      MoneyRepository moneyRepository = new MoneyRepository();

      //Act
      moneyRepository.ResetCredit();

      //Asert
      var credit = moneyRepository.GetAvailableCredit();
      Assert.AreEqual(credit, 0);
    }

    [TestMethod]
    public void ResetCash_Always_ShouldSetCashToZero()
    {
      //Arrange
      MoneyRepository moneyRepository = new MoneyRepository();

      //Act
      moneyRepository.ResetCash();

      //Assert
      var cash=moneyRepository.GetAvailableCash();
      Assert.AreEqual(cash, 0);
    }

    [TestMethod]
    public void GetAvailableCredit_Always_ShouldReturnCredit()
    {
      //Arrange
      MoneyRepository moneyRepository = new MoneyRepository();

      //Act
      moneyRepository.Add(4.5m, PaymentMethod.Credit);
      moneyRepository.Add(4.5m, PaymentMethod.Credit);
      moneyRepository.Add(4.5m, PaymentMethod.Cash);

      //Assert
      var credit=moneyRepository.GetAvailableCredit();
      credit.Should().Be(9m);
    }

    [TestMethod ]
    public void GetAvailableCash_Always_ShouldReturnCash()
    {
      //Arrange
      MoneyRepository moneyRepository = new MoneyRepository();

      //Act
      moneyRepository.Add(4.5m, PaymentMethod.Cash);
      moneyRepository.Add(4.5m, PaymentMethod.Cash);
      moneyRepository.Add(4.5m, PaymentMethod.Credit);

      //Assert
      var cash = moneyRepository.GetAvailableCash();
      cash.Should().Be(9m);
    }

    [TestMethod]
    public void Add_IfPeymentMethodIsCash_ShouldAddToCash()
    {
      //Arrange
      MoneyRepository moneyRepository = new MoneyRepository();

      //Act
      moneyRepository.Add(4.5m, PaymentMethod.Cash);
      moneyRepository.Add(4.5m, PaymentMethod.Credit);

      //Assert
      var cash = moneyRepository.GetAvailableCash();
      cash.Should().Be(4.5m);
    }

    [TestMethod]
    public void Add_IfPeymentMethodIsCredit_ShouldAddToCredit()
    {
      //Arrange
      MoneyRepository moneyRepository = new MoneyRepository();

      //Assert
      moneyRepository.Add(4.5m, PaymentMethod.Cash);
      moneyRepository.Add(4.5m, PaymentMethod.Credit);

      //Act
      var credit = moneyRepository.GetAvailableCredit();
      credit.Should().Be(4.5m);
    }
  }
}
