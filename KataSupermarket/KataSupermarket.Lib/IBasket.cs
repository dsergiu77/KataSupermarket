using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KataSupermarket.Lib
{
    public interface IBasket
    {
        void Add(string item);

        void Remove(string item);

        IEnumerable<KeyValuePair<string, int>> GetItems();

        BasketInfo GetInfo();

        decimal GetTotalPrice(IPricingEngine pe);
    }
}
