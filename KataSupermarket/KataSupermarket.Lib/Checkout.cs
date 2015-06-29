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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <param name="count"></param>
        /// <param name="undoScan"></param>
        /// <remarks>for multiple undo scan, use positive value for count and set parameter undoScan to true</remarks>
        public void Scan(string item, int count = 1, bool undoScan = false)
        {
            if (count <= 0)
                throw new ArgumentOutOfRangeException("count", "count must be > 0");

            for (int i = 0; i < count; i++)
            {
                if (!undoScan)
                {
                    basket.Add(item);
                }
                else
                {
                    basket.Remove(item);
                }
            }
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
