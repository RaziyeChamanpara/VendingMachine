using Models;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess
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
    public void Remove(Can can)
    {
      var purchaisedCan = _cans.Where(x => x.Id == can.Id).FirstOrDefault();
      purchaisedCan.Count--;

      if (purchaisedCan.Count == 0)
        _cans.Remove(purchaisedCan);
    }
    public void UpdateCount(Can can)
    {
      _cans.Where(x => x.Id == can.Id).FirstOrDefault().Count = can.Count;
     
    }
  }
}
