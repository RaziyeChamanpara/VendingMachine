using VendingMachine.DataAccess;
using VendingMachine.Models;
using System.Collections.Generic;
using System.Linq;

namespace VendingMachine.BusinessLogic
{
  public class VendingMachineLogic
  {
    MoneyRepository MoneyRepository { get; set; } = new MoneyRepository();
    CanRepository CanRepository { get; set; } = new CanRepository();

    private static VendingMachineLogic _VendingMachineLogic = new VendingMachineLogic();
    private VendingMachineLogic()
    {

    }
    public static VendingMachineLogic GetInstance()
    {
      return _VendingMachineLogic;
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
    public void Sell(Can can, PaymentMethod paymentMethod)
    {
      CanRepository.Remove(can.Id);
      MoneyRepository.Add(can.Price, paymentMethod);
    }
    public void Restock(List<Can> restockCans)
    {
      foreach (var restockCan in restockCans.ToList())
      {
        var can=CanRepository.Get(restockCan.Id);

        can.Count += restockCan.Count;

        CanRepository.Update(can);
      }

      MoneyRepository.ResetCash();

      MoneyRepository.ResetCredit();
    }
  }
}
