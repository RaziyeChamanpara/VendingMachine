using VendingMachine.Models;
using System.Collections.Generic;
using System.Linq;

namespace VendingMachine.DataAccess
{
  public class CanRepository
  {
    private readonly List<Can> _cans = new List<Can>()
    {
      new Can { Name = "Coka Cola", Id=1, Count = 3, Price = 4.5m },
      new Can { Name = "water",Id=2, Count = 2, Price = 4.5m },
      new Can { Name = "orange juice",Id=3, Count = 1, Price = 4.5m },
      new Can { Name = "apple juice",Id=4, Count = 1, Price = 4.5m },
      new Can { Name = "pepsi",Id=5, Count = 2, Price = 4.5m },
      new Can { Name = "lemonade",Id=6, Count = 1, Price = 4.5m },
    };
    private static CanRepository _canRepository = new CanRepository();
    private CanRepository()
    {

    }
    public CanRepository(List<Can> cans)
    {
      _cans = cans;
    }
    public static CanRepository GetInstance()
    {
      return _canRepository;
    }
    public List<Can> GetAll()
    {
      return _cans.Select(can=> new Can()
      {
        Id = can.Id,
        Count=can.Count,
        Name=can.Name,
        Price=can.Price
        
      }).ToList();
    }
    public Can Get(int id)
    {
      return _cans.Where(x => x.Id == id).FirstOrDefault();
    }
    public void Remove(int id)
    {
      var removingCan = _cans.Where(x => x.Id == id).FirstOrDefault();

      if (removingCan == null || removingCan.Count == 0)
        return;

      removingCan.Count--;
    }
    public void Update(Can newCan)
    {
      var oldCan=_cans.Where(x => x.Id == newCan.Id).FirstOrDefault(); 

      oldCan.Count = newCan.Count;
      oldCan.Price = newCan.Price;
      oldCan.Name = newCan.Name;
    }
  }
}
