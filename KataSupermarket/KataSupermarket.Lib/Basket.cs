using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KataSupermarket.Lib
{
    public class Basket : IBasket
    {
        Dictionary<string, int> itemsDict;
        BasketInfo info;

        public Basket()
        {
            itemsDict = new Dictionary<string, int>();
            info = new BasketInfo(this);
        }

        public void Add(string item)
        {
            if (itemsDict.ContainsKey(item))
            {
                itemsDict[item]++;
            }
            else
            {
                itemsDict.Add(item, 1);
            }
        }

        public void Remove(string item)
        {
            if (itemsDict.ContainsKey(item))
            {
                itemsDict[item]--;
                if (itemsDict[item] == 0)
                {
                    itemsDict.Remove(item);
                }
            }
            else
            {
                throw new InvalidOperationException(item + " is not present in the basket");
            }
        }

        public IEnumerable<KeyValuePair<string, int>> GetItems()
        {
            return itemsDict.AsEnumerable();
        }

        public BasketInfo GetInfo()
        {
            return info;
        }

        public decimal GetTotalPrice(IPricingEngine pe)
        {
            decimal totalPrice = 0;

            foreach (var item in this.itemsDict)
            {
                string sku = item.Key;
                int totalQuantity = item.Value;

                totalPrice += pe.GetItemTotalPrice(sku, totalQuantity);
            }

            return totalPrice;
        }
    }
}
