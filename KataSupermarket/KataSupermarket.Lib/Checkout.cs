using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KataSupermarket.Lib
{
    public class Checkout : ICheckout
    {
        #region Variables
        Basket basket;
        IPricingEngine pricingEngine;
        #endregion

        #region C-tors
        public Checkout(IPricingEngine pricingEngine)
        {
            basket = new Basket();
            this.pricingEngine = pricingEngine;
        }
        #endregion

        #region Public methods
        public decimal GetTotalPrice()
        {
            decimal totalPrice = basket.GetTotalPrice(this.pricingEngine);

            return totalPrice;
        }

        public void Scan(string item)
        {
            basket.Add(item);
        }

        public BasketInfo GetBasketInfo()
        {
            return basket.GetInfo();
        }

        public IPricingEngine GetPricingEngine()
        {
            return pricingEngine;
        }
        #endregion

        #region Private Methods
        #endregion
    }
}
