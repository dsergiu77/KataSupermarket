using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KataSupermarket.Lib
{
    public interface IPricingRule
    {
        string Sku { get; }

        decimal Price { get; }

        decimal GetPrice();

    }
}
