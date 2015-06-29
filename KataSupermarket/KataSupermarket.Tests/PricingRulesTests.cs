using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using KataSupermarket.Lib;

namespace KataSupermarket.Tests
{
    [TestClass]
    public class PricingRulesTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void QuantityPricingRule_Quantity_LessThan_1_Throws_Exception()
        {
            QuantityPricingRule rule = new QuantityPricingRule("A", 0, 50);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void QuantityPricingRule_Price_LessThan_0_Throws_Exception()
        {
            QuantityPricingRule rule = new QuantityPricingRule("A", 2, -1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void QuantityDiscountPricingRule_Quantity_LessThan_1_Throws_Exception()
        {
            QuantityDiscountPricingRule rule = new QuantityDiscountPricingRule("A", 0, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void QuantityDiscountPricingRule_DiscountQuantity_LessThan_1_Throws_Exception()
        {
            QuantityDiscountPricingRule rule = new QuantityDiscountPricingRule("A", 2, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void QuantityDiscountPricingRule_Quantity_LessThan_DiscountQuantity_Throws_Exception()
        {
            QuantityDiscountPricingRule rule = new QuantityDiscountPricingRule("A", 2, 3);
        }

        [TestMethod]
        public void If_QuantityPricingRule_A_50_Then_Price_is_50()
        {
            QuantityPricingRule rule = new QuantityPricingRule("A", 50);

            decimal price = rule.GetPrice();
            decimal expectedPrice = 50;

            Assert.AreEqual(expectedPrice, price);
        }

        [TestMethod]
        public void Define_SinglePriceRule_As_MultiPriceRule_With_Qty_1()
        {
            IPricingRule rule = new QuantityPricingRule("A", 1, 50);
            decimal price = rule.GetPrice();
            decimal expectedPrice = 50;

            Assert.AreEqual(expectedPrice, price);

        }

        [TestMethod]
        public void Define_QuantityDiscountPricingRule_A_3_for_the_Price_of_2()
        {
            QuantityDiscountPricingRule rule = new QuantityDiscountPricingRule("A", 3, 2);
            int discountQty = rule.GetTotalDiscountQuantity(3);
            decimal expectedDiscountQty = 2;

            Assert.AreEqual(expectedDiscountQty, discountQty);
        }

        [TestMethod]
        public void Define_QuantityDiscountPricingRule_A_5_for_the_Price_of_4()
        {
            QuantityDiscountPricingRule rule = new QuantityDiscountPricingRule("A", 5, 4);
            int discountQty = rule.GetTotalDiscountQuantity(5);
            decimal expectedDiscountQty = 4;

            Assert.AreEqual(expectedDiscountQty, discountQty);
        }

    }
}
