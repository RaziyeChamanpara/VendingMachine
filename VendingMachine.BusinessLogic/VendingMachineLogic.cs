using VendingMachine.DataAccess;
using VendingMachine.Models;
using System.Collections.Generic;
using System.Linq;

namespace VendingMachine.BusinessLogic
{
  public class VendingMachineLogic
  {
    public VendingMachineLogic(List<Can> cans)
    {
      CanRepository = new CanRepository(cans);
      MoneyRepository = new MoneyRepository();
    }
    public VendingMachineLogic()
    {
      CanRepository = CanRepository.GetInstance();
      MoneyRepository = MoneyRepository.GetInstance();
    }

    MoneyRepository MoneyRepository { get; set; }
    CanRepository CanRepository { get; set; }

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
    public bool Sell(Can can, PaymentMethod paymentMethod)
    {
      if (!(CanRepository.Remove(can.Id)))
        return false;

      MoneyRepository.Add(can.Price, paymentMethod);
      return true;
    }
    public void Restock(List<Can> restockCans)
    {
      foreach (var restockCan in restockCans.ToList())
      {
        var can = CanRepository.Get(restockCan.Id);

        can.Count += restockCan.Count;

        CanRepository.Update(can);
      }

      MoneyRepository.ResetCash();

      MoneyRepository.ResetCredit();
    }
  }
}
