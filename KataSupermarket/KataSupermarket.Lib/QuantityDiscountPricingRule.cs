using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace KataSupermarket.Lib
{
    public class QuantityDiscountPricingRule : IPricingRule
    {
        public QuantityDiscountPricingRule(string sku, int quantity, int discountQuantity)
        {
            if (quantity < 1)
                throw new ArgumentOutOfRangeException("quantity", "quantity must be > 0");
            if (discountQuantity < 1)
                throw new ArgumentOutOfRangeException("discountQuantity", "discount quantity must be > 0");
            if (quantity <= discountQuantity)
                throw new ArgumentOutOfRangeException("discountQuantity", "discount quantity must be < quantity");

            Sku = sku;
            Price = 0; // not relevant for this rule
            Quantity = quantity;
            DiscountQuantity = discountQuantity;
        }

        public string Sku { get; private set; }
        public decimal Price { get; private set; }
        public int Quantity { get; private set; }
        public int DiscountQuantity { get; private set; }

        public virtual decimal GetPrice()
        {
            return Price;
        }

        public virtual int GetTotalDiscountQuantity(int totalQuantity)
        {
            int discountQty = (totalQuantity / Quantity) * DiscountQuantity + (totalQuantity % Quantity);
            return discountQty;
        }
    }
}
