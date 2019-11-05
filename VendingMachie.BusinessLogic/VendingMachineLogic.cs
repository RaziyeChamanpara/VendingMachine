using VendingMachie.DataAccess;
using VendingMachie.Models;
using System.Collections.Generic;
using System.Linq;

namespace VendingMachine.BusinessLogic
{
  public class VendingMachineLogic
  {
    MoneyRepository MoneyRepository { get; set; } = new MoneyRepository();
    CanRepository CanRepository { get; set; } = new CanRepository();

    private static VendingMachineLogic _vendingMachineLogic = new VendingMachineLogic();
    private VendingMachineLogic()
    {

    }
    public static VendingMachineLogic GetInstance()
    {
      return _vendingMachineLogic;
    }
    public void Sell(Can can, PaymentMethod paymentMethod)
    {
      CanRepository.Remove(can);
      MoneyRepository.Add(can.Price, paymentMethod);
    }
    public List<Can> GetAllCans()
    {
      return CanRepository.GetAll();
    }
    public List<Can> GetAvailableCans()
    {
      return CanRepository.GetAll().Where(x => x.Count > 0).ToList();
    }
    public decimal GetAvailableCash()
    {
      return MoneyRepository.GetAvailableCash();
    }
    public decimal GetAvailableCredit()
    {
      return MoneyRepository.GetAvailableCredit();
    }
    public void Restock(List<Can> newCans)
    {
      foreach (var newCan in newCans.ToList())
      {
        var oldCan=CanRepository.Get(newCan.Id);

        oldCan.Count += newCan.Count;

        CanRepository.UpdateCount(oldCan);
      }

      MoneyRepository.ResetCash();

      MoneyRepository.ResetCredit();
    }
  }
}
