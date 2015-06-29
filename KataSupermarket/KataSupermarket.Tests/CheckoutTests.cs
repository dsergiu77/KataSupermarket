using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using KataSupermarket.Lib;
using System.Collections.Generic;

namespace KataSupermarket.Tests
{
    [TestClass]
    public class CheckoutTests
    {
        [TestMethod]
        public void If_Scan_Nothing_Then_Price_is_0()
        {
            ICheckout co = new Checkout(new PricingEngine());

            decimal price = co.GetTotalPrice();
            decimal expectedPrice = 0;

            Assert.AreEqual(expectedPrice, price);
        }

        [TestMethod]
        public void If_Scan_A_Then_Price_is_50()
        {
            ICheckout co = new Checkout(new PricingEngine());
            co.GetPricingEngine().AddQuantityPricingRule(new QuantityPricingRule("A", 50));

            co.Scan("A");
            decimal price = co.GetTotalPrice();
            decimal expectedPrice = 50;

            Assert.AreEqual(expectedPrice, price);
        }

        [TestMethod]
        public void If_Scan_AB_Then_Price_is_80()
        {
            IPricingEngine pe = new PricingEngine(
                new List<IPricingRule> { 
                    new QuantityPricingRule("A", 50),
                    new QuantityPricingRule("B", 30)
                });
            ICheckout co = new Checkout(pe);
            co.Scan("A");
            co.Scan("B");
            decimal price = co.GetTotalPrice();
            decimal expectedPrice = 80;

            Assert.AreEqual(expectedPrice, price);
        }

        [TestMethod]
        public void If_Scan_CDBA_Then_Price_is_115()
        {
            IPricingEngine pe = new PricingEngine();
            pe.AddQuantityPricingRule(new QuantityPricingRule("A", 50));
            pe.AddQuantityPricingRule(new QuantityPricingRule("B", 30));
            pe.AddQuantityPricingRule(new QuantityPricingRule("C", 20));
            pe.AddQuantityPricingRule(new QuantityPricingRule("D", 15));

            ICheckout co = new Checkout(pe);
            co.Scan("C");
            co.Scan("D");
            co.Scan("B");
            co.Scan("A");
            decimal price = co.GetTotalPrice();
            decimal expectedPrice = 115;

            Assert.AreEqual(expectedPrice, price);
        }

        [TestMethod]
        public void If_QuantityPricingRule_A_50_And_QuantityPricingRule_B_30_And_Scan_AB_Then_Price_is_80()
        {
            IPricingEngine pe = new PricingEngine();
            pe.AddQuantityPricingRule(new QuantityPricingRule("A", 50));
            pe.AddQuantityPricingRule(new QuantityPricingRule("B", 30));

            ICheckout co = new Checkout(pe);

            co.Scan("A");
            co.Scan("B");

            decimal price = co.GetTotalPrice();
            decimal expectedPrice = 80;

            Assert.AreEqual(expectedPrice, price);
        }


        [TestMethod]
        public void If_QuantityPricingRule_A_50_And_Scan_AA_Then_Price_is_100()
        {
            IPricingEngine pe = new PricingEngine();
            pe.AddQuantityPricingRule(new QuantityPricingRule("A", 50));
            pe.AddQuantityPricingRule(new QuantityPricingRule("B", 30));

            ICheckout co = new Checkout(pe);

            co.Scan("A");
            co.Scan("A");

            decimal price = co.GetTotalPrice();
            decimal expectedPrice = 100;

            Assert.AreEqual(expectedPrice, price);
        }

        [TestMethod]
        public void If_QuantityPricingRule_A_50_And_MultiPriceRule_A_3_130_And_Scan_AAA_Then_Price_is_130()
        {
            IPricingEngine pe = new PricingEngine(
                new List<IPricingRule> {
                    new QuantityPricingRule("A", 3, 130),
                    new QuantityPricingRule("A", 50)
                    }
                );
            ICheckout co = new Checkout(pe);
            co.Scan("A");
            co.Scan("A");
            co.Scan("A");

            decimal price = co.GetTotalPrice();
            decimal expectedPrice = 130;

            Assert.AreEqual(expectedPrice, price);
        }

        [TestMethod]
        public void If_QuantityPricingRule_A_50_And_MultiPriceRule_A_3_130_And_Scan_AAAA_Then_Price_is_180()
        {
            IPricingEngine pe = new PricingEngine();
            pe.AddQuantityPricingRule(new QuantityPricingRule("A", 50));
            pe.AddQuantityPricingRule(new QuantityPricingRule("A", 3, 130));

            ICheckout co = new Checkout(pe);

            co.Scan("A");
            co.Scan("A");
            co.Scan("A");
            co.Scan("A");

            decimal price = co.GetTotalPrice();
            decimal expectedPrice = 180;

            Assert.AreEqual(expectedPrice, price);
        }

        [TestMethod]
        public void If_QuantityPricingRule_A_50_And_MultiPriceRule_A_3_130_And_Scan_AAAAA_Then_Price_is_230()
        {
            IPricingEngine pe = new PricingEngine();
            pe.AddQuantityPricingRule(new QuantityPricingRule("A", 50));
            pe.AddQuantityPricingRule(new QuantityPricingRule("A", 3, 130));

            ICheckout co = new Checkout(pe);

            co.Scan("A");
            co.Scan("A");
            co.Scan("A");
            co.Scan("A");
            co.Scan("A");

            decimal price = co.GetTotalPrice();
            decimal expectedPrice = 230;

            Assert.AreEqual(expectedPrice, price);
        }

        [TestMethod]
        public void If_QuantityPricingRule_A_50_And_MultiPriceRule_A_3_130_And_Scan_AAAAA_AThen_Price_is_260()
        {
            IPricingEngine pe = new PricingEngine();
            pe.AddQuantityPricingRule(new QuantityPricingRule("A", 50));
            pe.AddQuantityPricingRule(new QuantityPricingRule("A", 3, 130));

            ICheckout co = new Checkout(pe);

            co.Scan("A");
            co.Scan("A");
            co.Scan("A");
            co.Scan("A");
            co.Scan("A");
            co.Scan("A");

            decimal price = co.GetTotalPrice();
            decimal expectedPrice = 260;

            Assert.AreEqual(expectedPrice, price);
        }

        [TestMethod]
        public void If_QuantityPricingRule_B_30_And_MultiPriceRule_A_3_130_And_Scan_AAAB_Then_Price_is_160()
        {
            IPricingEngine pe = new PricingEngine();
            pe.AddQuantityPricingRule(new QuantityPricingRule("A", 50));
            pe.AddQuantityPricingRule(new QuantityPricingRule("B", 30));
            pe.AddQuantityPricingRule(new QuantityPricingRule("A", 3, 130));

            ICheckout co = new Checkout(pe);

            co.Scan("A");
            co.Scan("A");
            co.Scan("A");
            co.Scan("B");

            decimal price = co.GetTotalPrice();
            decimal expectedPrice = 160;

            Assert.AreEqual(expectedPrice, price);
        }

        [TestMethod]
        public void If_MultiPriceRule_B_2_45_And_MultiPriceRule_A_3_130_And_Scan_AAABB_Then_Price_is_175()
        {
            IPricingEngine pe = new PricingEngine();
            pe.AddQuantityPricingRule(new QuantityPricingRule("A", 50));
            pe.AddQuantityPricingRule(new QuantityPricingRule("B", 30));
            pe.AddQuantityPricingRule(new QuantityPricingRule("A", 3, 130));
            pe.AddQuantityPricingRule(new QuantityPricingRule("B", 2, 45));

            ICheckout co = new Checkout(pe);

            co.Scan("A");
            co.Scan("A");
            co.Scan("A");
            co.Scan("B");
            co.Scan("B");

            decimal price = co.GetTotalPrice();
            decimal expectedPrice = 175;

            Assert.AreEqual(expectedPrice, price);
        }

        [TestMethod]
        public void If_MultiPriceRule_B_2_45_And_MultiPriceRule_A_3_130_And_Scan_AAABBD_Then_Price_is_190()
        {
            IPricingEngine pe = new PricingEngine();
            pe.AddQuantityPricingRule(new QuantityPricingRule("A", 50));
            pe.AddQuantityPricingRule(new QuantityPricingRule("B", 30));
            pe.AddQuantityPricingRule(new QuantityPricingRule("D", 15));
            pe.AddQuantityPricingRule(new QuantityPricingRule("A", 3, 130));
            pe.AddQuantityPricingRule(new QuantityPricingRule("B", 2, 45));

            ICheckout co = new Checkout(pe);

            co.Scan("A");
            co.Scan("A");
            co.Scan("A");
            co.Scan("B");
            co.Scan("B");
            co.Scan("D");

            decimal price = co.GetTotalPrice();
            decimal expectedPrice = 190;

            Assert.AreEqual(expectedPrice, price);
        }

        [TestMethod]
        public void If_MultiPriceRule_B_2_45_And_MultiPriceRule_A_3_130_And_Scan_DABABA_Then_Price_is_190()
        {
            IPricingEngine pe = new PricingEngine();
            pe.AddQuantityPricingRule(new QuantityPricingRule("A", 50));
            pe.AddQuantityPricingRule(new QuantityPricingRule("B", 30));
            pe.AddQuantityPricingRule(new QuantityPricingRule("D", 15));
            pe.AddQuantityPricingRule(new QuantityPricingRule("A", 3, 130));
            pe.AddQuantityPricingRule(new QuantityPricingRule("B", 2, 45));

            ICheckout co = new Checkout(pe);

            co.Scan("D");
            co.Scan("A");
            co.Scan("B");
            co.Scan("A");
            co.Scan("B");
            co.Scan("A");

            decimal price = co.GetTotalPrice();
            decimal expectedPrice = 190;

            Assert.AreEqual(expectedPrice, price);
        }

        [TestMethod]
        public void If_Add_A_To_Basket_Then_Basket_Has_A_Once()
        {
            IBasket basket = new Basket();
            basket.Add("A");
            int countA = basket.GetInfo().GetCount("A");
            int expectedCountA = 1;

            Assert.AreEqual(expectedCountA, countA);
        }

        [TestMethod]
        public void Checkout_Has_BasketInfo()
        {
            ICheckout co = new Checkout(new PricingEngine());

            BasketInfo info = co.GetBasketInfo();

            Assert.IsNotNull(info);
        }

        [TestMethod]
        public void If_Scan_A_Then_BasketInfo_Has_A_Once()
        {
            ICheckout co = new Checkout(new PricingEngine());
            co.Scan("A");
            BasketInfo info = co.GetBasketInfo();

            int countA = info.GetCount("A");
            int expectedCountA = 1;

            Assert.AreEqual(expectedCountA, countA);
        }

        [TestMethod]
        public void If_MultiPriceRule_A_3_130_And_MultiPriceRule_A_5_190_And_Scan_AAAAA_Then_Price_is_190()
        {
            IPricingEngine pe = new PricingEngine(
                new List<IPricingRule> { 
                    new QuantityPricingRule("A", 50),
                    new QuantityPricingRule("A", 3, 130),
                    new QuantityPricingRule("A", 5, 190),
                });

            ICheckout co = new Checkout(pe);

            co.Scan("A");
            co.Scan("A");
            co.Scan("A");
            co.Scan("A");
            co.Scan("A");

            decimal price = co.GetTotalPrice();
            decimal expectedPrice = 190;

            Assert.AreEqual(expectedPrice, price);
        }

        [TestMethod]
        public void If_MultiPriceRule_A_3_130_And_MultiPriceRule_A_5_190_And_Scan_9A_Then_Price_is_370()
        {
            IPricingEngine pe = new PricingEngine(
                new List<IPricingRule> { 
                    new QuantityPricingRule("A", 50),
                    new QuantityPricingRule("A", 3, 130),
                    new QuantityPricingRule("A", 5, 190),
                });

            ICheckout co = new Checkout(pe);

            for (int i = 0; i < 9; i++)
            {
                co.Scan("A");
            }

            decimal price = co.GetTotalPrice();
            decimal expectedPrice = 370;

            Assert.AreEqual(expectedPrice, price);
        }

        [TestMethod]
        public void If_PriceCombination_And_Scan_9A7B_Then_Price_is_530()
        {
            IPricingEngine pe = new PricingEngine(
                new List<IPricingRule> { 
                    new QuantityPricingRule("A", 50),
                    new QuantityPricingRule("A", 3, 130),
                    new QuantityPricingRule("A", 5, 190),
                    new QuantityPricingRule("B", 30),
                    new QuantityPricingRule("B", 2, 45),
                    new QuantityPricingRule("B", 4, 85)
                });

            ICheckout co = new Checkout(pe);

            for (int i = 0; i < 9; i++)
            {
                co.Scan("A");
            }
            for (int i = 0; i < 7; i++)
            {
                co.Scan("B");
            }

            decimal price = co.GetTotalPrice();
            decimal expectedPrice = 530;

            Assert.AreEqual(expectedPrice, price);
        }

        [TestMethod]
        public void If_PriceCombination_And_Scan_Random_9A7B_Then_Price_is_530()
        {
            IPricingEngine pe = new PricingEngine(
                new List<IPricingRule> { 
                    new QuantityPricingRule("A", 50),
                    new QuantityPricingRule("A", 3, 130),
                    new QuantityPricingRule("A", 5, 190),
                    new QuantityPricingRule("B", 30),
                    new QuantityPricingRule("B", 2, 45),
                    new QuantityPricingRule("B", 4, 85)
                });

            ICheckout co = new Checkout(pe);

            for (int i = 0; i < 7; i++)
            {
                co.Scan("A");
                co.Scan("B");
            }
            for (int i = 0; i < 2; i++)
            {
                co.Scan("A");
            }

            decimal price = co.GetTotalPrice();
            decimal expectedPrice = 530;

            Assert.AreEqual(expectedPrice, price);
        }

        [TestMethod]
        public void If_MultiPriceRules_for_A_3_130_And_Scan_9A_Then_Price_is_370()
        {
            IPricingEngine pe = new PricingEngine(
                new List<IPricingRule> { 
                    new QuantityPricingRule("A", 50),
                    new QuantityPricingRule("A", 3, 130),
                    new QuantityPricingRule("A", 5, 190),
                });

            ICheckout co = new Checkout(pe);

            for (int i = 0; i < 9; i++)
            {
                co.Scan("A");
            }

            decimal price = co.GetTotalPrice();
            decimal expectedPrice = 370;

            Assert.AreEqual(expectedPrice, price);
        }

        [TestMethod]
        public void If_QuantityDiscountPricingRule_A_3_2_And_Scan_3A_Then_Price_is_90()
        {
            IPricingEngine pe = new PricingEngine();
            pe.AddQuantityDiscountPricingRule(new QuantityDiscountPricingRule("A", 3, 2));
            pe.AddQuantityPricingRule(new QuantityPricingRule("A", 3, 130));
            pe.AddQuantityPricingRule(new QuantityPricingRule("A", 2, 90));
            pe.AddQuantityPricingRule(new QuantityPricingRule("A", 1, 50));

            ICheckout co = new Checkout(pe);

            for (int i = 0; i < 3; i++)
            {
                co.Scan("A");
            }

            decimal price = co.GetTotalPrice();
            decimal expectedPrice = 90;

            Assert.AreEqual(expectedPrice, price);
        }

        [TestMethod]
        public void If_QuantityDiscountPricingRule_A_5_4_And_A_3_2_And_Scan_5A_Then_Price_is_180()
        {
            IPricingEngine pe = new PricingEngine(
                new List<IPricingRule> { 
                    new QuantityDiscountPricingRule("A", 3, 2),
                    new QuantityPricingRule("A", 3, 130),
                    new QuantityPricingRule("A", 2, 90),
                    new QuantityPricingRule("A", 1, 50),
                    new QuantityDiscountPricingRule("A", 5, 4),
                });

            ICheckout co = new Checkout(pe);

            for (int i = 0; i < 5; i++)
            {
                co.Scan("A");
            }

            decimal price = co.GetTotalPrice();
            decimal expectedPrice = 180;

            Assert.AreEqual(expectedPrice, price);
        }
    }
}
