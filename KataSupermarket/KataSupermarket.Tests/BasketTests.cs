using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using KataSupermarket.Lib;

namespace KataSupermarket.Tests
{
    /// <summary>
    /// Summary description for BasketTests
    /// </summary>
    [TestClass]
    public class BasketTests
    {
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
        [ExpectedException(typeof(InvalidOperationException))]
        public void If_Remove_Item_Not_Existing_In_Basket_Then_Throw_Exception()
        {
            IBasket basket = new Basket();
            basket.Remove("A");
        }

        [TestMethod]
        public void If_Add_A_And_Remove_A_Then_Basket_is_Empty()
        {
            IBasket basket = new Basket();
            basket.Add("A");
            basket.Remove("A");

            int countA = basket.GetInfo().GetCount("A");
            int expectedCountA = 0;

            Assert.AreEqual(expectedCountA, countA);
        }

    }
}
