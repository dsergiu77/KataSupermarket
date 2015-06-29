using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using KataSupermarket.Lib;
using System.Collections.Generic;

namespace KataSupermarket.Tests
{
    [TestClass]
    public class PricingEngineTests
    {
        [TestMethod]
        public void If_PricingEngine_Has_No_PricingRules_Then_ItemTotalPrice_Is_0()
        {
            IPricingEngine pe = new PricingEngine();

            decimal price = pe.GetItemTotalPrice("B", 3);
            decimal expectedPrice = 0;

            Assert.AreEqual(expectedPrice, price);
        }

        [TestMethod]
        public void If_PricingEngine_Has_No_PricingRules_For_A_Then_TotalPrice_For_A_Is_0()
        {
            IPricingEngine pe = new PricingEngine();

            decimal price = pe.GetItemTotalPrice("A", 3);
            decimal expectedPrice = 0;

            Assert.AreEqual(expectedPrice, price);
        }

        [TestMethod]
        public void PricingEngine_Instantiate_From_List()
        {
            IPricingEngine pe = new PricingEngine(
                new List<IPricingRule> { 
                    new QuantityPricingRule("A", 50),
                    new QuantityPricingRule("B", 30)
                });
            decimal price = pe.GetItemTotalPrice("A", 2);
            decimal expectedPrice = 100;

            Assert.AreEqual(expectedPrice, price);
        }

        [TestMethod]
        public void PricingEngine_Add_Generic_PricingRules()
        {
            IPricingEngine pe = new PricingEngine();
            pe.AddPricingRule(new QuantityPricingRule("A", 50));
            pe.AddPricingRule(new QuantityPricingRule("B", 30));

            decimal price = pe.GetItemTotalPrice("B", 3);
            decimal expectedPrice = 90;

            Assert.AreEqual(expectedPrice, price);
        }

        [TestMethod]
        public void PricingEngine_Add_Specific_PricingRules()
        {
            IPricingEngine pe = new PricingEngine();
            pe.AddQuantityPricingRule(new QuantityPricingRule("A", 50));
            pe.AddQuantityPricingRule(new QuantityPricingRule("A", 2, 80));
            pe.AddQuantityDiscountPricingRule(new QuantityDiscountPricingRule("A", 3, 2));

            decimal price = pe.GetItemTotalPrice("A", 3);
            decimal expectedPrice = 80;

            Assert.AreEqual(expectedPrice, price);
        }
    }
}
