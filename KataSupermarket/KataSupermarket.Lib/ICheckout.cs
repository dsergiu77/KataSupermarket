using System;
namespace KataSupermarket.Lib
{
    public interface ICheckout
    {
        BasketInfo GetBasketInfo();

        decimal GetTotalPrice();

        void Scan(string item, int count = 1, bool undoScan = false);

        IPricingEngine GetPricingEngine();
    }
}
