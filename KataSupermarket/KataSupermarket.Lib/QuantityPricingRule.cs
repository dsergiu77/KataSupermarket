using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace KataSupermarket.Lib
{
    public class QuantityPricingRule : IPricingRule
    {
        public QuantityPricingRule(string sku, decimal price)
            : this(sku, 1, price)
        {
        }

        public QuantityPricingRule(string sku, int quantity, decimal price)
        {
            if (quantity < 1)
                throw new ArgumentOutOfRangeException("quantity", "quantity must be > 0");
            if (price < 0)
                throw new ArgumentOutOfRangeException("price", "price must be >= 0");

            Sku = sku;
            Price = price;
            Quantity = quantity;
        }

        public string Sku { get; private set; }
        public decimal Price { get; private set; }
        public int Quantity { get; private set; }

        public virtual decimal GetPrice()
        {
            return Price;
        }
    }
}
