using Models;
using System.Collections.Generic;
namespace DataAccess
{
  public class MoneyRepository
  {
    private decimal _cash = 0;
    private decimal _credit = 0;

    public decimal GetAvailableCash()
    {
      return _cash;
    }
    public decimal GetAvailableCredit()
    {
      return _credit;
    }
    public void Add(decimal price, PaymentMethod paymentMethod)
    {
      if (paymentMethod == PaymentMethod.Cash)
        _cash += price;

      else
        _credit += price;
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
