using System;
using VendingMachine.Models;
namespace VendingMachine.DataAccess
{
  public class MoneyRepository
  {
    private decimal _cash = 0;
    private decimal _credit = 0;
    private static MoneyRepository _moneyRepository=new MoneyRepository();
    public static MoneyRepository GetInstance()
    {
      return _moneyRepository;
    }

    public MoneyRepository()
    {

    }

    public void Add(decimal price, PaymentMethod paymentMethod)
    {
      if (paymentMethod == PaymentMethod.Cash)
        _cash += price;

      else
        _credit += price;
    }

    

    public decimal GetAvailableCash()
    {
      return _cash;
    }
    public decimal GetAvailableCredit()
    {
      return _credit;
    }
    public void ResetCash()
    {
      _cash = 0;
    }
    public void ResetCredit()
    {
      _credit = 0;
    }
  }
}
