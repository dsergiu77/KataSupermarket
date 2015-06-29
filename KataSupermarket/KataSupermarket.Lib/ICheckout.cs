using System;
namespace KataSupermarket.Lib
{
    public interface ICheckout
    {
        BasketInfo GetBasketInfo();

        decimal GetTotalPrice();

        void Scan(string item);

        IPricingEngine GetPricingEngine();
    }
}
