using System;
using System.Collections.Generic;

namespace KataSupermarket.Lib
{
    public interface IPricingEngine
    {
        void AddPricingRule(IPricingRule rule);

        void AddQuantityDiscountPricingRule(QuantityDiscountPricingRule rule);

        void AddQuantityPricingRule(QuantityPricingRule rule);

        decimal GetItemTotalPrice(string item, int totalQuantity);
    }
}
