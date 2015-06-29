using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KataSupermarket.Lib
{
    public class BasketInfo
    {
        Basket basket;
        public BasketInfo(Basket basket)
        {
            this.basket = basket;
        }

        public int GetCount(string item)
        {
            int count = basket.GetItems().Count(x => x.Key == item);

            return count;
        }
    }
}
